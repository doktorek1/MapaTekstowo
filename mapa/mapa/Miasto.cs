using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    public class Miasto
    {
        private int id;
        private string nazwa;

        public Miasto(String nazwa, int id)
        {
            this.nazwa = nazwa;
            this.id = id;
        }

        public String dajNazwe()
        {
            return nazwa;
        }

        public int dajId()
        {
            return id;
        }
    }
}
