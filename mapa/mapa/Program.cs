using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    public class Program
    {
        static void Main(string[] args)
        {
            int iloscSamochodow = 2;
            int pojemnoscSamochodu = 2;
            
            try
            {
                string s = args[1];
                string s2 = args[2];
                iloscSamochodow = Convert.ToInt32(s);
                pojemnoscSamochodu = Convert.ToInt32(s2);
            }
            catch (IndexOutOfRangeException e)
            {                
                Console.WriteLine("B³êdne dane w przy wywo³aniu! Przyjmujê wartoœci domyœlne");
            }
            catch (FormatException)
            {
                iloscSamochodow = 2;
                pojemnoscSamochodu = 2;
            }
            String plik;
            int[,] mapp = null;
            try
            {
                plik = args[0];
                Wczytywanie w = new Wczytywanie(plik);
                w.ReadData();
                w.ReadPackages();
                Tworzenie tworzenie = new Tworzenie(w);//na podstawie zebranych danych tworzy mapê miast dla potrzeb programu
                Kopiec kopiec = new Kopiec();
                 mapp = tworzenie.utworzMape();//metoda tworz¹ca mapê, zwraca dwuwymiarow¹ tablicê - mapê - mapp
                List<Zlecenie> z = w.DajZlecenia();//pobiera listê zleceñ, (obiekty), po wczytaniu
                kopiec.wprowadzDaneDoKopca(z);//zapis obiektów powy¿szej listy na kopiec
                //===========================================================================            
                Dijkstra dijkstra = new Dijkstra(mapp);
                Wezel[,] wezel = dijkstra.obliczOdlegloscPomiedzyMiastami(5);
                //wezel = dijkstra.obliczOdlegloscPomiedzyMiastami(0);
                List<Miasto> miasta = w.DajMiasta();// wczytana lista miast
                Przewoz przewoz = new Przewoz();
                List<int> powrot = przewoz.dajWierzcholkiPoDrodze();
                //===============================
                FlotaSamochodow flotaaa = new FlotaSamochodow();
                //if(args.Length == 2)                

                int iloscPaczek = z.Count;
                //Console.WriteLine(iloscPaczek);
                flotaaa.utworzFlote(iloscSamochodow);
                Jedz jedz = new Jedz(); //int yy = 1;
                List<Zlecenie> zleceniaZKopca = kopiec.dajKopiec();

                for (int x = 0; x < iloscPaczek / (pojemnoscSamochodu * iloscSamochodow); x++)//iloscPaczek / (pojemnoscSamochodu * iloscSamochodow)
                {
                    //Console.WriteLine(x);
                    for (int i = 0; i < pojemnoscSamochodu; i++)
                        for (int j = 0; j < iloscSamochodow; j++)
                        {
                            //Console.WriteLine(yy); yy++;
                            flotaaa.dodajZlecenieDoSamochodu(kopiec.sciagnijZWierzcholkaKopca(), j);
                        }
                    //rozwieŸ i wróæ
                    jedz.rozwiez(flotaaa, iloscSamochodow, mapp, w.DajIloscMiast() - 2, miasta);
                    //if (zleceniaZKopca.Count == 0)
                    //  break;
                }
                z = kopiec.dajKopiec();
                Console.WriteLine(z.Count);
                iloscSamochodow = z.Count / pojemnoscSamochodu;
                for (int n = 0; n < iloscSamochodow; n++)
                    flotaaa.flota[n].dajSamochodZPaczkami().Clear();
                for (int i = 0; i < pojemnoscSamochodu; i++)
                    for (int j = 0; j < iloscSamochodow; j++)
                    {
                        //Console.WriteLine(yy); yy++;
                        flotaaa.dodajZlecenieDoSamochodu(kopiec.sciagnijZWierzcholkaKopca(), j);
                    }
                //rozwieŸ i wróæ
                jedz.rozwiez(flotaaa, iloscSamochodow, mapp, w.DajIloscMiast() - 2, miasta);
                //if (zleceniaZKopca.Count == 0)
                //OSTATNI SAMOchód
                //  break;
                z = kopiec.dajKopiec();
                Console.WriteLine(z.Count);

                for (int n = 0; n < iloscSamochodow; n++)
                    flotaaa.flota[n].dajSamochodZPaczkami().Clear();
                iloscSamochodow = 1;
                flotaaa.utworzFlote(iloscSamochodow);
                Zlecenie e = kopiec.sciagnijZWierzcholkaKopca();
                for (int i = 0; i < pojemnoscSamochodu; i++)
                    for (int j = 0; j < iloscSamochodow; j++)
                    {
                        //Console.WriteLine(yy); yy++;
                        e = kopiec.sciagnijZWierzcholkaKopca();
                        if (e == null) break;
                        flotaaa.dodajZlecenieDoSamochodu(e, j);
                    }
                //rozwieŸ i wróæ
                if (e != null)
                    jedz.rozwiez(flotaaa, iloscSamochodow, mapp, w.DajIloscMiast() - 2, miasta);
                //if (zleceniaZKopca.Count == 0)
                //  break;

                //===========================================================================
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Nie podano pliku wejœciowego, Restartuj program ");
            }
            //Visualization v = new Visualization(mapp);
            //v.rysujGraf();
            Console.Write("Naciœnij dowolny przycisk, aby zakoñczyæ program...");
            Console.ReadKey();
        }
    }
}












/*sci¹ga z kopca dane
 * ================================================================
 * foreach (Zlecenie value in z)
            {
                Zlecenie d = kopiec.sciagnijZWierzcholkaKopca();
                Console.WriteLine(d.dajPriorytet());
            }
 * 
 * =========================================================
 * for (int x = 0; x < w.DajIloscMiast(); x++)
            {
                for (int y = 0; y < w.DajIloscMiast(); y++)
                {
                    Console.Write(mapp[x, y] + " ");
                }
                Console.WriteLine();
 * ==============================================
 * kod do prostego testowania œci¹gania z kopca
 * ============================================
 *  List<int> tab = new List<int>();
            tab.Add(10);
            tab.Add(8);
            tab.Add(5);
            tab.Add(-1);
            tab.Add(2);
            tab.Add(4);
            tab.Add(4);
            
            for (int r = 0; r < 7; r++)
            {
                //foreach (int value in tab)
                   // Console.Write(value + ",");
                //Console.WriteLine();
                int szczyt = tab[0];//zapamiêtujê korzeñ, tzn ten który zabieram
                int top = tab[0];
                tab[0] = tab[tab.Count - 1];//zamieniam z ostatnim liœciem                
                tab.RemoveAt(tab.Count - 1);//usuwam liœæ
                int n = 0;
                int maks = -1;

                while (n < tab.Count )
                {
                    int t = 2*n+1;
                    

                    if (2 * n + 1 < tab.Count && tab[n] < tab[2 * n + 1])
                    { maks = 2 * n + 1; t = maks; }
                    if (2 * n + 2 < tab.Count && tab[n] < tab[2 * n + 2] && tab[2 * n + 1] < tab[2 * n + 2])
                    { maks = 2 * n + 2; t = maks; }
                    if(maks > 0)
                    {
                        int tmp = tab[n];
                        tab[n] = tab[maks];
                        tab[maks] = tmp;
                    }
                    n = t;
                }
                Console.WriteLine(top);
            }
 * 
 * =============================================================================================
 * 
            }*/

/* for (int o = 0; o < 50; o++ )
            {
                //yy++;
                //Zlecenie d = kopiec.sciagnijZWierzcholkaKopca();
                //Console.WriteLine(yy + " " + d.dajPriorytet());

            }
            
            
            /*for (int x = 0; x < w.DajIloscMiast(); x++)
            {
                for (int y = 0; y < w.DajIloscMiast(); y++)
                {
                    Console.Write(mapp[x, y] + " ");
                }
                Console.WriteLine();
            }*/

/*
 foreach (int value in powrot)
                Console.Write(value + ", ");
            Console.WriteLine();
            for (int i = 0; i < w.DajIloscMiast() - 1; i++)
            {
                for (int j = 0; j < w.DajIloscMiast(); j++)
                {
                    if (i == w.DajIloscMiast() - 2)
                        Console.Write("<" + wezel[i, j].dajOdleglosc() + ">" + "(" + wezel[i, j].dajPoprzedniWierzcholek() + ")");
                    //else
                    //Console.Write("<" + wezel[i, j].dajOdleglosc() + "> ");
                }
                // Console.WriteLine();
            }
*/