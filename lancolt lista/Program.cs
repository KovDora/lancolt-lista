using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _21f_Lancolt_lista
{
	internal class Program
	{

		class Lancolt_lista<S>
		{
			class Elem<T>
			{
				public Elem<T> elozo;
				public T tartalom;
				public Elem<T> kovetkezo;

				public Elem(Elem<T> elozo, T tartalom, Elem<T> kovetkezo)
				{
					this.elozo = elozo;
					this.tartalom = tartalom;
					this.kovetkezo = kovetkezo;
				}

				public Elem()
				{
					this.elozo = this;
					this.kovetkezo = this;
				}

				public Elem(Elem<T> kijelolt, T tartalom)
				{
					this.tartalom = tartalom;
					Elem<T> új = this;
					új.elozo = kijelolt;
					új.kovetkezo = kijelolt.kovetkezo;
					kijelolt.kovetkezo.elozo = új;
					kijelolt.kovetkezo = új;
				}

				public void Torol()
				{
					this.kovetkezo.elozo = this.elozo;
					this.elozo.kovetkezo = this.kovetkezo;
				}
			}

			private Elem<S> fejelem;
			public int Count;

			public Lancolt_lista()
			{
				fejelem = new Elem<S>();
				Count = 0;
			}

			private Elem<S> Utolso_elem()
			{
				return fejelem.elozo;
			}

			public void Add(S tartalom)
			{
				Elem<S> elem = new Elem<S>(Utolso_elem(), tartalom);
				Count++;
			}

			public S First()
			{
				if (Count == 0)
				{
					throw new IndexOutOfRangeException();
				}
				return fejelem.kovetkezo.tartalom;
			}

			public S Last()
			{
				if (Count == 0)
				{
					throw new IndexOutOfRangeException();
				}
				return fejelem.elozo.tartalom;
			}

			public S Element_at(int n)
			{

				if (Count <= n)
				{
					throw new IndexOutOfRangeException();
				}

				Elem<S> aktelem = fejelem.kovetkezo;

				for (int i = 0; i < n; i++)
				{
					aktelem = aktelem.kovetkezo;
				}

				return aktelem.tartalom;
			}
			public void Diagnosztika()
			{
				Console.WriteLine($"Count: {Count}");
				Console.WriteLine($"Elemek:");
				Elem<S> aktelem = fejelem.kovetkezo;
				for (int i = 0; i < Count; i++)
				{
					Console.WriteLine($"{(aktelem.elozo == fejelem ? "FEJELEM" : aktelem.elozo.tartalom.ToString())} -> {aktelem.tartalom.ToString()} -> {(aktelem.kovetkezo == fejelem ? "FEJELEM" : aktelem.kovetkezo.tartalom.ToString())}");
					aktelem = aktelem.kovetkezo;
				}
			}

			public void RemoveAt(int n)
			{
				if (Count <= n)
				{
					throw new IndexOutOfRangeException();
				}

				Elem<S> aktelem = fejelem.kovetkezo;

				for (int i = 0; i < n; i++)
				{
					aktelem = aktelem.kovetkezo;
				}
				aktelem.Torol();
				Count--;
			}

			public void Remove(S t)
			{
				Elem<S> aktelem = fejelem.kovetkezo;

				while (aktelem != fejelem && !aktelem.tartalom.Equals(t))
				{
					aktelem = aktelem.kovetkezo;
				}
				aktelem.Torol();
				Count--;
			}

			public void RemoveAll(S t)
			{
				Elem<S> aktelem = fejelem.kovetkezo;

				while (aktelem != fejelem)
				{
					if (aktelem.tartalom.Equals(t))
					{
						aktelem.Torol();
						Count--;
					}
					aktelem = aktelem.kovetkezo;
				}
			}

			public override string ToString()
			{
				string s = "[";

				Elem<S> aktelem = fejelem.kovetkezo;
				for (int i = 0; i < Count - 1; i++)
				{
					s += $" {aktelem.tartalom},";
					aktelem = aktelem.kovetkezo;
				}
				if (0 < Count)
				{
					s += $" {aktelem.tartalom}";
				}
				s += " ]";
				return s;
			}

			public S Max(Func<S, S, int> rendezesi_szempont)
			{
				if (Count == 0)
				{
					throw new IndexOutOfRangeException();
				}
				S legjobb = fejelem.kovetkezo.tartalom; // ez az első elem!



				Elem<S> aktelem = fejelem.kovetkezo.kovetkezo; // ez a második elem!

				while (aktelem != fejelem) // Addig keresgélünk, míg a fejelemhez nem érünk.
				{
					if (rendezesi_szempont(legjobb, aktelem.tartalom) == -1) // hogyan mondjuk, hogy jobb? Kacsacsőr nem fog menni.
					{
						legjobb = aktelem.tartalom;
					}
					aktelem = aktelem.kovetkezo; // "i++"
				}
				return legjobb;
			}
		}

		static int Számok_szokasos_rendezése(int a, int b)
		{
			if (a < b) return -1;
			if (a > b) return 1;
			return 0;
		}


		static void Main(string[] args)
		{
			Lancolt_lista<int> lista = new Lancolt_lista<int>();
			lista.Add(6);
			lista.Add(8);
			lista.Add(7);
			lista.Add(3);
			lista.Add(4);
			lista.Add(1);

			lista.RemoveAll(8);

			/*
			Console.WriteLine(lista);
			lista.Diagnosztika();
			Console.WriteLine(lista.Max(Számok_szokasos_rendezése));
			*/
		}
	}
}
