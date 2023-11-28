using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Termek> kosar = new List<Termek>();

    static void Main(string[] args)
    {
        Console.WriteLine("Üdvözöljük a vásárlási alkalmazásban!");

        while (true)
        {
            Console.WriteLine("\nVálasszon egy műveletet:");
            Console.WriteLine("1. Termék hozzáadása a kosárhoz");
            Console.WriteLine("2. Kosár tartalma");
            Console.WriteLine("3. Végösszeg kiszámítása");
            Console.WriteLine("4. Kilépés");

            string valasztas = Console.ReadLine();

            switch (valasztas)
            {
                case "1":
                    HozzaadTermek();
                    break;
                case "2":
                    KosarTartalma();
                    break;
                case "3":
                    Vegosszeg();
                    break;
                case "4":
                    MentKilepesElott();
                    return;
                default:
                    Console.WriteLine("Érvénytelen választás. Kérjük, válasszon újra.");
                    break;
            }
        }
    }

    static void HozzaadTermek()
    {
        Console.WriteLine("\nVálasszon egy terméket a következők közül:");
        Console.WriteLine("1. Laptop");
        Console.WriteLine("2. Mobiltelefon");
        Console.WriteLine("3. Televízió");
        Console.WriteLine("4. Könyv");

        string termekValasztas = Console.ReadLine();

        switch (termekValasztas)
        {
            case "1":
                AdjonHozzaTermeket("Laptop");
                break;
            case "2":
                AdjonHozzaTermeket("Mobiltelefon");
                break;
            case "3":
                AdjonHozzaTermeket("Televízió");
                break;
            case "4":
                AdjonHozzaTermeket("Könyv");
                break;
            default:
                Console.WriteLine("Érvénytelen választás. Kérjük, válasszon újra.");
                break;
        }
    }

    static void AdjonHozzaTermeket(string termekNev)
    {
        Console.WriteLine($"Adja meg a {termekNev} árát:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal ar))
        {
            Console.WriteLine("Érvénytelen ár formátum.");
            return;
        }

        Console.WriteLine($"Adja meg a {termekNev} márkáját:");
        string marka = Console.ReadLine();

        Termek ujTermek = new Termek(termekNev, ar, marka);
        kosar.Add(ujTermek);
        Console.WriteLine($"{termekNev} hozzáadva a kosárhoz.");
    }

    static void KosarTartalma()
    {
        Console.WriteLine("\nKosár tartalma:");
        foreach (var termek in kosar)
        {
            Console.WriteLine($"{termek.Nev} - {termek.Marka} - {termek.Ar} Ft");
        }
    }

    static void Vegosszeg()
    {
        decimal osszeg = 0;
        foreach (var termek in kosar)
        {
            osszeg += termek.Ar;
        }
        Console.WriteLine($"\nVégösszeg: {osszeg} Ft");
    }

    static void MentKilepesElott()
    {
        Console.WriteLine("\nKívánja menteni a kosár tartalmát? (Igen/Nem)");
        string valasz = Console.ReadLine();
        if (valasz.ToLower() == "igen")
        {
            MentKosar();
        }
    }

    static void MentKosar()
    {
        string fajlNev = "kosar.txt";

        if (!File.Exists(fajlNev))
        {
            File.Create(fajlNev).Close();
        }

        using (StreamWriter sw = new StreamWriter(fajlNev))
        {
            foreach (var termek in kosar)
            {
                sw.WriteLine($"{termek.Nev},{termek.Ar},{termek.Marka}");
            }
        }
        Console.WriteLine("A kosár tartalma el lett mentve a 'kosar.txt' fájlba.");
    }

}

class Termek
{
    public string Nev { get; set; }
    public decimal Ar { get; set; }
    public string Marka { get; set; }

    public Termek(string nev, decimal ar, string marka)
    {
        Nev = nev;
        Ar = ar;
        Marka = marka;
    }
}
