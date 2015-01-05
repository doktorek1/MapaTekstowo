using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Mapa
{
    class Jedz
    {
        FlotaSamochodow samochody = new FlotaSamochodow();
        List<Zlecenie> zlecenia;
        Stopwatch stopwatch = Stopwatch.StartNew();
        int yy = 1;

        public void rozwiez(FlotaSamochodow flotaaa, int iloscSamochodow, int[,] mapp, int iloscMiast, List<Miasto> listaMiast)
        {
            Dijkstra dijkstra = new Dijkstra(mapp);//wczytujemy do algorytmu Dijkstry mapę
            Przewoz przewoz = new Przewoz();
            Wezel[,] wezel;

            for (int i = 0; i < iloscSamochodow; i++)
            {
                zlecenia = flotaaa.flota[i].dajSamochodZPaczkami();
                int start = zlecenia[0].dajPoczatek();
                int start2 = start;

                while (flotaaa.flota[i].dajSamochodZPaczkami().Count > 0)
                {
                    Console.WriteLine(yy); yy++;
                    flotaaa.polozenieSamochodu(zlecenia[0].dajCel(), i);
                    wezel = dijkstra.obliczOdlegloscPomiedzyMiastami(start2);
                    przewoz.okreslDrogePowrotna(start2, zlecenia[0].dajCel(), wezel, iloscMiast);
                    List<int> powrotne = new List<int>();
                    powrotne = przewoz.dajWierzcholkiPoDrodze();

                    Console.WriteLine("Samochód nr: " + (i + 1) + " Przebieg: " + flotaaa.odczytajPrzebiegZTablicyPojazdow(i) + " Pobrano przesyłkę " + zlecenia[0].dajId() + " z miasta " + (listaMiast[start2].dajId() + 1));
                    if (start2 != zlecenia[0].dajCel())
                        flotaaa.dodajOdleglosc(wezel[iloscMiast, zlecenia[0].dajCel()].dajOdleglosc(), i);
                    else
                        flotaaa.dodajOdleglosc(0, i);
                    Console.WriteLine("Samochód nr: " + (i + 1) + " Przebieg: " + flotaaa.odczytajPrzebiegZTablicyPojazdow(i) + " Dostarczono przesyłkę " + zlecenia[0].dajId() + " do miasta " + (listaMiast[zlecenia[0].dajCel()].dajId() + 1));
                    start2 = zlecenia[0].dajCel();
                    stopwatch = Stopwatch.StartNew();
                    Thread.Sleep(wezel[iloscMiast, zlecenia[0].dajCel()].dajOdleglosc());
                    stopwatch.Stop();
                    zlecenia.RemoveAt(0);
                    Console.WriteLine();

                }
                //powrót do miasta startowego po następne paczki, także dodajemy odległość
                wezel = dijkstra.obliczOdlegloscPomiedzyMiastami(flotaaa.flota[i].dajPolozenieSamochodu());
                flotaaa.dodajOdleglosc(wezel[iloscMiast, start].dajOdleglosc(), i);//dopisujemy do przebiegu odległość powrotną, tzn musimy wrócić do miasta-bazy po następne paczki
                flotaaa.polozenieSamochodu(start, i);
                //Console.WriteLine(wezel[iloscMiast, start].dajOdleglosc());
                //Console.WriteLine(flotaaa.flota[i].dajPolozenieSamochodu());
            }
        }
    }
}


/* foreach (int value in powrotne)
                    {
                        Console.Write(value + " ");
                        flaga = true;
                        for (int u = 0; u < zlecenia.Count; u++)
                        {
                            if (value == zlecenia[u].dajCel())
                                zlecenia.RemoveAt(u);
                            if (zlecenia.Count == 1)
                                break;
                        }
                        if (zlecenia.Count == 1)
                            break;
                    }
                    Console.WriteLine();
                    //if (flaga == true)
                        //continue;
                    if(zlecenia.Count == 0)
                        Console.WriteLine("PUUUUUUUUUUUUUUUsto");*/
