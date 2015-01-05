using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    class Dijkstra      //niech nikt nie pyta, jak działa metoda obliczOdlegloscPomiedzyMiastami(), bo to za trudne :D
    {
        int[,] tab;
        public Dijkstra(int[,] tab)
        {
            this.tab = tab;
        }

        public Wezel[,] obliczOdlegloscPomiedzyMiastami(int idMiastaPoczatkowego)//idMiastaPoczatkowego - miasto od którego mierzymy odleglości do innych miast
        {
            int zablokowanaKolumna = idMiastaPoczatkowego;
            int d = (int)(Math.Sqrt(tab.Length));
            Wezel[,] tablicaRobocza = new Wezel[d, d];
            int odleglosc = 0, poprzedni = 0;
            //zaczynamy od wypełnienia pierwszego wiersza
            for (int i = 0; i < d; i++)
            {
                odleglosc = -1;
                poprzedni = -1;
                if (i != idMiastaPoczatkowego)
                {
                    if (tab[i, idMiastaPoczatkowego] != -1)
                    {
                        odleglosc = tab[i, idMiastaPoczatkowego];
                        poprzedni = idMiastaPoczatkowego;
                    }
                    tablicaRobocza[0, i] = new Wezel(odleglosc, poprzedni, false);
                }
                else
                {
                    tablicaRobocza[0, i] = new Wezel(idMiastaPoczatkowego, -1, true);//kolumna, zawierająca miasto startowe
                }
            }
            int min;
            min = 0;
            int index = idMiastaPoczatkowego;
            for (int i = 0; i < d; i++)
            {
                if (tablicaRobocza[0, i].dajOdleglosc() > 0 && i != idMiastaPoczatkowego)
                {
                    min = tablicaRobocza[0, i].dajOdleglosc();
                }
            }
            for (int i = 0; i < d; i++)
            {
                if ((i != idMiastaPoczatkowego) && (tablicaRobocza[0, i].dajOdleglosc() <= min) && (tablicaRobocza[0, i].dajOdleglosc() > 0))
                {
                    min = tablicaRobocza[0, i].dajOdleglosc();
                    index = i;
                }
            }
            tablicaRobocza[0, index].ustawFlage(true);

            idMiastaPoczatkowego = index;
            //-------------------------wypełniamy dalsze wiersze tablicy----------------------------------------------
            int dotychczasowaOdleglosc = 0;

            for (int i = 1; i < d - 1; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    dotychczasowaOdleglosc = tablicaRobocza[i - 1, index].dajOdleglosc();
                    if (tab[index, j] != -1 && tablicaRobocza[i - 1, j].dajFlage() != true)
                    {

                        if (dotychczasowaOdleglosc + tab[index, j] > tablicaRobocza[i - 1, j].dajOdleglosc() && tablicaRobocza[i - 1, j].dajOdleglosc() != -1)
                        {
                            tablicaRobocza[i, j] = tablicaRobocza[i - 1, j];
                            //Console.WriteLine(tablicaRobocza[i - 1, j].dajOdleglosc() + ", " + i);
                        }
                        else
                            tablicaRobocza[i, j] = new Wezel(dotychczasowaOdleglosc + tab[index, j], index, false);
                    }
                    else
                    {
                        tablicaRobocza[i, j] = tablicaRobocza[i - 1, j];
                    }
                }
                index = 0;
                for (int h = 0; h < d; h++)
                {
                    if (tablicaRobocza[i, h].dajOdleglosc() > 0 && h != zablokowanaKolumna && tablicaRobocza[i - 1, h].dajFlage() != true)
                    {
                        min = tablicaRobocza[i, h].dajOdleglosc();
                    }
                }
                for (int j = 0; j < d; j++)//znajduje minimalny element w wierszu nie pierwszym
                {
                    if (j != zablokowanaKolumna && tablicaRobocza[i - 1, j].dajFlage() != true && (tablicaRobocza[i, j].dajOdleglosc() <= min) && tablicaRobocza[i, j].dajOdleglosc() > 0)
                    {
                        min = tablicaRobocza[i, j].dajOdleglosc();
                        index = j;
                    } 
                }

                tablicaRobocza[i, index].ustawFlage(true);
                idMiastaPoczatkowego = index;
            }
            return tablicaRobocza;
        }
    }
}








/*


 for (int j = 0; j < d; j++)
                {
                        dotychczasowaOdleglosc = tablicaRobocza[i - 1, index].dajOdleglosc();
                    if (tab[i, index] != -1 && tablicaRobocza[i - 1, j].dajFlage() != true && dotychczasowaOdleglosc + tab[i, j] < tablicaRobocza[i-1, j].dajOdleglosc())
                        tablicaRobocza[i, j] = new Wezel(dotychczasowaOdleglosc + tab[i, j], zablokowanaKolumna, false);
                    else
                        tablicaRobocza[i, j] = tablicaRobocza[i - 1, j];
                } 
                Console.WriteLine(dotychczasowaOdleglosc + " " + index + " " + min);
                index = 0;
                for (int h = 0; h < d; h++)
                {
                    if (tablicaRobocza[i, h].dajOdleglosc() > 0 && h != zablokowanaKolumna && tablicaRobocza[i - 1, h].dajFlage() != true)
                    {
                        min = tablicaRobocza[i, h].dajOdleglosc();
                    } 
                }
                //Console.WriteLine(min);
                for (int j = 0; j < d; j++)//znajduje minimalny element w wierszu
                {
                    if (j != zablokowanaKolumna && tablicaRobocza[i - 1, j].dajFlage() != true && (tablicaRobocza[i, j].dajOdleglosc() <= min) && tablicaRobocza[i, j].dajOdleglosc() > 0)
                    {
                        min = tablicaRobocza[i, j].dajOdleglosc();
                        index = j;
                    } //Console.WriteLine(tablicaRobocza[i - 1, j].dajFlage());
                }
 * ----------------------
 * 
 * for (int j = 0; j < d; j++)
                {
                    if (tablicaRobocza[i - 1, j].dajFlage() == true)
                        tablicaRobocza[i, j] = tablicaRobocza[i - 1, j];
                    else
                        if (tab[index, j] != -1)
                            tablicaRobocza[i, j] = new Wezel(dotychczasowaOdleglosc + tab[index, i], idMiastaPoczatkowego, false);
                }

*/