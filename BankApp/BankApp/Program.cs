

// test user list
var users = new List<Tuple<int, string, string, DateTime>>()
{
    Tuple.Create(1, "Ante", "Antić", new DateTime(2001,09,21)),
    Tuple.Create(2, "Pero", "Perić", new DateTime(1999,10,22)),
    Tuple.Create(3, "Lovre", "Lovrić", new DateTime(2000,11,23)),
    Tuple.Create(4, "Karlo", "Karlović", new DateTime(2003,12,24)),
    Tuple.Create(5, "Karlo", "Ima vise od 30", new DateTime(1973,12,24))
};


//test account list
var accounts = new List<Tuple<int, string, double>>()
{
    Tuple.Create(1, "žiro", 0.0),
    Tuple.Create(1, "tekući", 2.0),
    Tuple.Create(2, "žiro", -200.0),
    Tuple.Create(1, "žiro", -1.0),
    Tuple.Create(3, "žiro", 1.0),
};





// Main menu
static void PrintMainMenu(bool error) // Prints out Main menu text
{
    Console.Clear();
    if (error)
        Console.WriteLine(" Pogrešno upisan odabir!");

    Console.WriteLine("\n GLAVNI IZBORNIK\n\n 1 - Korisnici\n 2 - Računi\n 3 - Izlaz iz aplikacije");
    Console.Write("\n\n Vaš odabir: ");
    
}

static void MainMenu(bool error, List<Tuple<int, string, string, DateTime>> users, List<Tuple<int, string, double>> accounts) // Main menu controller
{   
    PrintMainMenu(error);

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Clear();
            UserMenu(false, users, accounts);
            MainMenu(false, users, accounts);
            break;
        case "2":
            Console.Clear();
            Console.WriteLine(" Odabrali ste izbornik Računi.");
            Console.ReadKey();
            // handle option 2
            MainMenu(false, users, accounts);
            break;
        case "3":
            Console.Clear();
            Console.WriteLine(" Napustili ste aplikaciju.");
            break;
        default:
            MainMenu(true, users, accounts);
            break;
    }
}


// User menu

static void PrintUserMenu(bool error) // Prints out user menu
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

static void UserMenu(bool error, List<Tuple<int, string, string, DateTime>> users, List<Tuple<int, string, double>> accounts) // User menu controller
{
    PrintUserMenu(error);

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
            UserMenuOption(false, users, accounts);
            break;

        case "5": // EXIT TO MAIN MENU
            break;

        default:
            UserMenu(true, users, accounts);
            break;
    }

}

static void UserMenuOption(bool error, List<Tuple<int, string, string, DateTime>> users, List<Tuple<int, string, double>> accounts)
{
    Console.Clear();
    if (error) 
        Console.WriteLine("\n Neispravan odabir! ");
    Console.WriteLine("\n\ta) ispis svih korisnika abecedno po prezimenu");
    Console.WriteLine("\tb) ispis svih onih koji imaju više od 30 godina");
    Console.WriteLine("\tc) ispis svih onih koji imaju barem jedan račun u minusu");
    Console.WriteLine("\td) izlaz");

    var option = Console.ReadLine();

    switch (option) 
    {
        case "a":
            Console.Clear();
            PrintUsersBySurname(users);
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            UserMenu(false, users, accounts);
            break;

        case "b": 
            Console.Clear();
            PrintUsersOverThirty(users);
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            UserMenu(false, users, accounts);
            break;
        case "c":
            Console.Clear();
            PrintUsersInMinus(users, accounts);
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            UserMenu(false, users, accounts);
            break;
        case "d":
            UserMenu(false, users, accounts);
            break;
        default:
            UserMenuOption(true,users, accounts);
            break;
    }
}

// User data printing

static void PrintUsersBySurname(List<Tuple<int, string, string, DateTime>> users) // Prints out the user list alphabeticaly by surname
 {
    foreach (var user in users.OrderBy(x => x.Item3))
        Console.WriteLine($" {user.Item1} - {user.Item2} - {user.Item3} - {user.Item4.ToString("dd/MM/yyyy")}");
}

static void PrintUsersOverThirty(List<Tuple<int, string, string, DateTime>> users) // Prints out users in the user list that are 30+ years old
{
    foreach (var user in users.Where(x => (DateTime.Today - x.Item4) >= new TimeSpan(30*365,0,0,0)))
        Console.WriteLine($" {user.Item1} - {user.Item2} - {user.Item3} - {user.Item4.ToString("dd/MM/yyyy")}");
}

static void PrintUsersInMinus(List<Tuple<int, string, string, DateTime>> users, List<Tuple<int, string, double>> accounts) // Prints out users that own accounts which are in minus
{
    var visited = new List<int>();

    foreach(var account in accounts.Where(x => x.Item3 < 0))
    {
        if(visited.Contains(account.Item1))
            continue;
        visited.Add(account.Item1);

        var user = users.FirstOrDefault(x => x.Item1 == account.Item1, null);

        if (user != null)
            Console.WriteLine($" {user.Item1} - {user.Item2} - {user.Item3} - {user.Item4.ToString("dd/MM/yyyy")}");

    }

}



// Start of program

MainMenu(false, users, accounts);
