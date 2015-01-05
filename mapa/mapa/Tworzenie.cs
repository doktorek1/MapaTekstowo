using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    class Tworzenie
    {
        Wczytywanie wczytywanie;

        public Tworzenie(Wczytywanie wczytywanie)
        {
            this.wczytywanie = wczytywanie;
        }

        public int[,] utworzMape()
        {
            int i = wczytywanie.DajIloscMiast();
            int [,] mapa = new int [i, i];
            List<Polaczenie> polaczenia = new List<Polaczenie>();
            polaczenia = wczytywanie.DajPolaczenia();
            for (int x = 0; x < i; x++)
            {
                for (int y = 0; y < i; y++)
                {
                    mapa[x, y] = -1;
                }
            }
            for (int x = 0; x < polaczenia.Count; x++)
            {
                //Console.WriteLine(polaczenia.Count);
                mapa[polaczenia[x].dajPoczatek(), polaczenia[x].dajCel()] = polaczenia[x].dajOdleglosc();
                mapa[polaczenia[x].dajCel(), polaczenia[x].dajPoczatek()] = polaczenia[x].dajOdleglosc();
            }            
                return mapa;
        }
    }
}
