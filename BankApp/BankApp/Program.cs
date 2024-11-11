static void PrintMainMenu(bool error)
{
    Console.Clear();
    if (error)
        Console.WriteLine(" Pogrešno upisan odabir!");

    Console.WriteLine("\n GLAVNI IZBORNIK\n\n 1 - Korisnici\n 2 - Računi\n 3 - Izlaz iz aplikacije");
    Console.Write("\n\n Vaš odabir: ");
    
}

static void MainMenu(bool error)
{   
    PrintMainMenu(error);

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Clear();
            UserMenu(false);
            MainMenu(false);
            break;
        case "2":
            Console.Clear();
            Console.WriteLine(" Odabrali ste izbornik Računi.");
            Console.ReadKey();
            // handle option 2
            MainMenu(false);
            break;
        case "3":
            Console.Clear();
            Console.WriteLine(" Napustili ste aplikaciju.");
            break;
        default:
            MainMenu(true);
            break;
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
    Console.WriteLine(" 5 - Natrag");
}

static void UserMenu(bool error)
{
    PrintUserMenu(false);

    //Console.ReadKey();
    var option = Console.ReadLine();

    switch (option) 
    {
        case "1": // ADD NEW USER
            Console.Clear();
            Console.WriteLine("\n Odabran je unos novog korisnika.");
            // TODO: implement adding a user
            Console.ReadKey();
            break;

        case "2": // DELETE USER 
            Console.Clear();
            Console.WriteLine("Odabrano je brisanje korisnika.");
            // TODO: implement deleting a user
            Console.ReadKey();
            break;

        case "3": // EDIT USER
            Console.Clear();
            Console.WriteLine("Odabrano uređivanje korisnika.");
            // TODO: implement editing a user
            Console.ReadKey();
            break;

        case "4": // PRINT USERS
            Console.Clear();
            Console.WriteLine("Odabran pregled korisnika.");
            // TODO: implement printing all users
            Console.ReadKey();
            break;

        case "5": // EXIT TO MAIN MENU
            break;

        default:
            UserMenu(false);
            break;
    }

}



MainMenu(false);