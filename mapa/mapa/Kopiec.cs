using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    public class Kopiec
    {
        private List<Zlecenie> Kopiec_Ze_Zleceniami = new List<Zlecenie>();
        Zlecenie tmp;
        /*public Kopiec(Zlecenie Kopiec_Ze_Zleceniami)
        {
            this.Kopiec_Ze_Zleceniami = Kopiec_Ze_Zleceniami;
        }*/

        public void dodajZlecenieDoKopca(Zlecenie zlecenie)
        {
            Kopiec_Ze_Zleceniami.Add(zlecenie);            
                int x = Kopiec_Ze_Zleceniami.Count - 1;//świeżo dodany element
                int y;
                if (x % 2 == 0)
                    y = (x - 2) / 2;
                else
                    y = (x - 1) / 2;

                if (Kopiec_Ze_Zleceniami.Count > 1)
                {
                    while (Kopiec_Ze_Zleceniami[x].dajPriorytet() > Kopiec_Ze_Zleceniami[y].dajPriorytet())
                    {
                        tmp = Kopiec_Ze_Zleceniami[x];
                        Kopiec_Ze_Zleceniami[x] = Kopiec_Ze_Zleceniami[y];
                        Kopiec_Ze_Zleceniami[y] = tmp;
                        x = y;
                        if (x % 2 == 0 && x > 1)
                            y = (x - 2) / 2;
                        else
                            if (x > 0)
                                y = (x - 1) / 2;
                    }
                }
            
        }
        public Zlecenie sciagnijZWierzcholkaKopca()
        {
            if (Kopiec_Ze_Zleceniami.Count == 0)
                return null;
            Zlecenie szczyt = Kopiec_Ze_Zleceniami[0];//zapamiętuję korzeń, tzn ten który zabieram
            Zlecenie top = Kopiec_Ze_Zleceniami[0];
            Kopiec_Ze_Zleceniami[0] = Kopiec_Ze_Zleceniami[Kopiec_Ze_Zleceniami.Count - 1];//zamieniam z ostatnim liściem                
            Kopiec_Ze_Zleceniami.RemoveAt(Kopiec_Ze_Zleceniami.Count - 1);//usuwam liść
            int n = 0;
            int maks = -1;

            while (n < Kopiec_Ze_Zleceniami.Count)
            {
                int t = 2 * n + 1;
                if (2 * n + 1 < Kopiec_Ze_Zleceniami.Count && Kopiec_Ze_Zleceniami[n].dajPriorytet() < Kopiec_Ze_Zleceniami[2 * n + 1].dajPriorytet())
                { maks = 2 * n + 1; t = maks; }
                if (2 * n + 2 < Kopiec_Ze_Zleceniami.Count && Kopiec_Ze_Zleceniami[n].dajPriorytet() < Kopiec_Ze_Zleceniami[2 * n + 2].dajPriorytet()
                    && Kopiec_Ze_Zleceniami[2 * n + 1].dajPriorytet() < Kopiec_Ze_Zleceniami[2 * n + 2].dajPriorytet())
                { maks = 2 * n + 2; t = maks; }
                if (maks > 0)
                {
                    Zlecenie tmp = Kopiec_Ze_Zleceniami[n];
                    Kopiec_Ze_Zleceniami[n] = Kopiec_Ze_Zleceniami[maks];
                    Kopiec_Ze_Zleceniami[maks] = tmp;
                }
                n = t;
            }
            return top;
        }
        //public Zlecenie dajWierzcholekKopca(){}
        public List<Zlecenie> dajKopiec()
        {
            return Kopiec_Ze_Zleceniami;
        }
        public void wprowadzDaneDoKopca(List<Zlecenie> z)//uzupełnia kopiec danymi
        {
            foreach (Zlecenie value in z)
                dodajZlecenieDoKopca(value);
        }
        
    }
}
