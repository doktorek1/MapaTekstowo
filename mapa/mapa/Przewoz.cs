using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    class Przewoz//ta klasa służy do określenia drogi powrotnej (w praktyce służy do sprawdzania, czy można coś zostawić po drodze)
    {
        
        private List<int> wierzcholkiPowrotne = new List<int>();//tablica (lista) przechowująca wierzchołki, przez które przechodzi najkrótsza trasa do wierzchołka docelowego

        public void okreslDrogePowrotna(int poczatkowy, int koncowy, Wezel [,] wezel, int indeksOstatniegoWiersza){
            wierzcholkiPowrotne.Clear();
            /*poczatkowy - miasto, z którego startuje przesylka
             * koncowy - miasto do którego wieziemy przesyłkę
             * indeksOstatniegoWiersza - indeks ostatniego wiersza w tablicy z odległościami, w ostatnim wierszu są bowiem rzeczywiste odległości, (obiekty)
             * wezel - tablica dwuwymiarowa, w ostatnim wierszu ma obiekty z rzeczywistymi odległościami pomiędzy wierzchołkami
             */
            wierzcholkiPowrotne.Add(koncowy);
            while(poczatkowy != koncowy){
                if (wezel[indeksOstatniegoWiersza, koncowy].dajPoprzedniWierzcholek() == -1)
                    break;
                wierzcholkiPowrotne.Add(wezel[indeksOstatniegoWiersza, koncowy].dajPoprzedniWierzcholek());
                koncowy = wezel[indeksOstatniegoWiersza, koncowy].dajPoprzedniWierzcholek();
            }
        }
        public List<int> dajWierzcholkiPoDrodze()
        {
            return wierzcholkiPowrotne;
        }
    }
}
