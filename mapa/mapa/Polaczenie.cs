using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    public class Polaczenie//klasa zawierająca definicje połączeń pomiędzy miastami
    {
        private int miasto1, miasto2, odleglosc;//definicja miasta początkowego, docelowego i odległości pomiędzy nimi
        public Polaczenie(int miasto1, int miasto2, int odleglosc)
        {
            this.miasto1 = miasto1;
            this.miasto2 = miasto2;
            this.odleglosc = odleglosc;
        }
        public int dajPoczatek()//zwraca id miasta początkowego
        {
            return miasto1;
        }
        public int dajCel()//zwraca id miasta końcowego
        {
            return miasto2;
        }
        public int dajOdleglosc()//zwraca odległość pomiędzy miastami
        {
            return odleglosc;
        }
    }
}
