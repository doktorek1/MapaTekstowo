using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    class Samochod
    {
        private List<Zlecenie> samochodZPaczkami = new List<Zlecenie>();
        private int polozenie = 0;//oznacza, gdzie się aktualnie znajduje samochód
        private double przejechanaOdleglosc = 0;//przejechana odleglosc przez samochod

        public List<Zlecenie> dajSamochodZPaczkami()
        {
            return samochodZPaczkami;
        }
        public void dodajPaczkeDoSamochodu(Zlecenie paczka)
        {
            samochodZPaczkami.Add(paczka);
        }
        public void ustawPolozenie(int polozenie)
        {
            this.polozenie = polozenie;
        }
        public void aktualizujOdleglosc(double odleglosc)
        {
            przejechanaOdleglosc = przejechanaOdleglosc + odleglosc;
        }
        public double dajPrzebieg()
        {
            return przejechanaOdleglosc;
        }
        public int dajPolozenieSamochodu()
        {
            return polozenie;
        }

    }
}
