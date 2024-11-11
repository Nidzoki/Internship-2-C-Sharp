
static void PrintMainMenu(bool error)
{
    Console.Clear();
    if (error)
        Console.WriteLine("Pogrešno upisan odabir!");

    Console.WriteLine("\n GLAVNI IZBORNIK\n\n 1 - Korisnici\n 2 - Računi\n 3 - Izlaz iz aplikacije");
    Console.Write("\n\n Vaš odabir: ");
    
}

static int MainMenu(bool error)
{
    PrintMainMenu(error);

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Clear();
            UserMenu(false);
            return MainMenu(false);
        case "2":
            Console.Clear();
            Console.WriteLine("Odabrali ste izbornik Računi.");
            Console.ReadKey();
            // handle option 2
            return MainMenu(false);
        case "3":
            Console.Clear();
            Console.WriteLine("Napustili ste aplikaciju.");
            return 0;
        default:
            return MainMenu(true);
    }
}

static void PrintUserMenu(bool error)
{
    Console.Clear();
    if (error)
        Console.WriteLine("\n Neispravan odabir!\n");
    Console.WriteLine("\n IZBORNIK KORISNICI\n\n 1 - Unos novog korisnika\n 2 - Brisanje korisnika");
    Console.WriteLine("\ta) po id-u\n\tb) po imenu i prezimenu\n 3 - Uređivanje korisnika\n\ta) po id-u\n 4 - Pregled korisnika");
    Console.WriteLine("\ta) ispis svih korisnika abecedno po prezimenu");
    Console.WriteLine("\tb) ispis svih onih koji imaju više od 30 godina");
    Console.WriteLine("\tc) ispis svih onih koji imaju barem jedan račun u minusu");
}

static void UserMenu(bool error)
{
    PrintUserMenu(false);

    Console.ReadKey();
    // implement handling user operations

}



MainMenu(false);