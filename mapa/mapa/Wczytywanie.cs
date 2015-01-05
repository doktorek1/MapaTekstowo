using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Mapa
{
    public class Wczytywanie
    {
        private String plik;
        private List<Miasto> miasta = new List<Miasto>();//tu przechowuję w tablicy miasta oraz ich identyfikatory
        private List<Polaczenie> polaczenia = new List<Polaczenie>();//i połączenia pomiędzy nimi
        private List<Zlecenie> zlecenia = new List<Zlecenie>();//lista zleceń
        private int miastoStartowe = -1;
        private int x = 1;//pomijamy pierwszy wiersz pliku
        private int iloscMiast;
        public Wczytywanie(String plik)
        {
            this.plik = plik;
        }
        public void ReadData()
        {
            //lines;
            int id = -1;

            string[] lines = System.IO.File.ReadAllLines(@"E:\Mapa\Mapa\miasta.txt");

            while (!String.IsNullOrEmpty(lines[x]))
            {

                try
                {
                    id = int.Parse(lines[x].Substring(0, lines[x].IndexOf(" ")));
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Błędne dane w pliku z mapą!");
                    break;
                }
                lines[x] = lines[x].Remove(0, lines[x].IndexOf(" ") + 1);
                miasta.Add(new Miasto(lines[x], id));
                //Console.WriteLine(id + ", " + lines[x]);
                x++;
            }
            iloscMiast = x - 1;
            x++;
            while (x < lines.Length && !String.IsNullOrEmpty(lines[x]))//początek odczytu połączeń między miastami
            {
                if (lines[x].IndexOf("#") != -1)//omijam linię ze znakiem "#"
                {
                    x++;
                    continue;
                }
                int foundS1 = lines[x].IndexOf(" ");
                int foundS2 = lines[x].IndexOf(" ", foundS1 + 1);
                int poczatek = 0, cel = 0, odleglosc = 0;
                try
                {
                    poczatek = int.Parse(lines[x].Substring(0, lines[x].IndexOf(" ")));
                    cel = int.Parse(lines[x].Substring(foundS1 + 1, lines[x].IndexOf(" ", foundS1 + 1) - foundS1));
                    odleglosc = int.Parse(lines[x].Substring(foundS2 + 1, lines[x].Length - 1 - foundS2));
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Błędne dane w pliku z mapą lub niepoprawny ich format!");
                    break;
                }
                polaczenia.Add(new Polaczenie(poczatek, cel, odleglosc));//dodaje do ArrayListy obiekt zawierający info o polaczeniach
                x++;
            }

        }

        public void ReadPackages()
        {
            x = 0;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(plik);//@"E:\Mapa\Mapa\przesylki.txt"
                try
                {
                    miastoStartowe = int.Parse(lines[x]);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Niepoprawne dane w pliku!");
                }
                //Console.WriteLine(miastoStartowe);
                x++;
                while (x < lines.Length && !String.IsNullOrEmpty(lines[x]))
                {
                    int foundS1 = lines[x].IndexOf(" ");
                    int foundS2 = lines[x].IndexOf(" ", foundS1 + 1);
                    int foundS3 = lines[x].IndexOf(" ", foundS2 + 1);
                    int id = 0, poczatek = 0, cel = 0;
                    try
                    {
                        id = int.Parse(lines[x].Substring(0, lines[x].IndexOf(" ")));
                        poczatek = int.Parse(lines[x].Substring(foundS1 + 1, lines[x].IndexOf(" ", foundS1 + 1) - foundS1));
                        cel = int.Parse(lines[x].Substring(foundS2 + 1, lines[x].IndexOf(" ", foundS2 + 1) - foundS2));
                    }
                    catch (System.FormatException e)
                    {
                        Console.WriteLine("Błędne dane w pliku z mapą lub niepoprawny ich format!");
                    }
                    lines[x] = lines[x].Remove(0, foundS3 + 1);
                    //Mamy teraz string zazwierający nazwę zlecenia i priorytet. Za pomocą wyrażęnia regularnego wybieramy liczbę i nazwę:
                    String pat = @"\d+";//wzór wyrażenia            
                    Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                    Match m = r.Match(lines[x]);
                    int priorytet = int.Parse(m.Value);//priorytet
                    pat = @"\D+";
                    r = new Regex(pat, RegexOptions.IgnoreCase);
                    m = r.Match(lines[x]);
                    String nazwaZlecenia = m.Value.Remove(m.Value.Length - 1, 1);
                    zlecenia.Add(new Zlecenie(id, poczatek, cel, priorytet, nazwaZlecenia));
                    //Console.WriteLine(id + ", " + poczatek + ", " + cel + ", " + nazwaZlecenia + ", " + priorytet);
                    x++;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Błędna nazwa pliku! ");
            }
        }
        public List<Miasto> DajMiasta()
        {
            return miasta;
        }
        public List<Polaczenie> DajPolaczenia()
        {
            return polaczenia;
        }
        public List<Zlecenie> DajZlecenia()
        {
            return zlecenia;
        }
        public int DajIloscMiast()
        {
            return iloscMiast;
        }

    }
}
