using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    public class Zlecenie : IComparable 
    {
        private int id, poczatek, cel, priorytet;
        private string nazwa;

        public Zlecenie(int id, int poczatek, int cel, int priorytet, String nazwa)
        {
            this.poczatek = poczatek;
            this.id = id;
            this.cel = cel;
            this.priorytet = priorytet;
            this.nazwa = nazwa;
        }
        public int CompareTo(Object obj)
        {            
            if (obj == null)
                return 1;
            Zlecenie inneZlecenie = obj as Zlecenie;
            return this.priorytet.CompareTo(inneZlecenie.priorytet);
        }
        public int dajId()
        {
            return id;
        }
        public int dajPoczatek()
        {
            return poczatek;
        }
        public int dajCel()
        {
            return cel;
        }
        public int dajPriorytet()
        {
            return priorytet;
        }
        public String dajNazwe()
        {
            return nazwa;
        }
    }
}
