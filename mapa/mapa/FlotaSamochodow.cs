using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapa
{
    class FlotaSamochodow
    {
        public List<Samochod> flota = new List<Samochod>();

        public void utworzFlote(int iloscSamochodow)
        {
            for (int i = 0; i < iloscSamochodow; i++)
            {
                flota.Add(new Samochod());
            }
        }
        public void dodajZlecenieDoSamochodu(Zlecenie zlecenie, int numerSamochodu)
        {
            flota[numerSamochodu].dodajPaczkeDoSamochodu(zlecenie);
            //Console.WriteLine(flota[numerSamochodu]);
        }
        public void dodajOdleglosc(int odleglosc, int numerSamochodu)
        {
            flota[numerSamochodu].aktualizujOdleglosc(odleglosc);
        }
        public void polozenieSamochodu(int polozenie, int numerSamochodu)//zapisuje w obiekcie "Samochod" aktualne położenie
        {
            flota[numerSamochodu].ustawPolozenie(polozenie);
        }
        public double odczytajPrzebiegZTablicyPojazdow(int numerSamochodu)//odczytuje z obiektu "Samochod" przebieg samochodu
        {
            return flota[numerSamochodu].dajPrzebieg();
        }
    }
}
