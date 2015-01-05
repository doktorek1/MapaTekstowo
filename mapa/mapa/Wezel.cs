using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    class Wezel
    {
        private int odleglosc, poprzedniWierzcholek;//odleglosc - odleglosc do punktu startowego, poprzedni wierzchołek- poprzedni wierzchołek, przez który prowadzi najkróttsza droga do początku 
        private bool czySprawdzony;//flaga informująca, czy już obliczono drogę do wierzchołka startowego
        public Wezel(int odleglosc, int poprzedniWierzcholek, bool czySprawdzony)
        {
            this.odleglosc = odleglosc;
            this.poprzedniWierzcholek = poprzedniWierzcholek;
            this.czySprawdzony = czySprawdzony;
        }

        public Boolean dajFlage()
        {
            return czySprawdzony;
        }

        public int dajPoprzedniWierzcholek()
        {
            return poprzedniWierzcholek;
        }
        public int dajOdleglosc()
        {
            return odleglosc;
        }
        public void ustawFlage(Boolean f)
        {
            czySprawdzony = f;
        }
        public void ustawOdleglosc(int f)
        {
            odleglosc = f;
        }
    }
}
