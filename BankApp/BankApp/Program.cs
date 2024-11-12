
// test user list

using System.Security.Principal;

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

static void MainMenu(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts) // Main menu controller
{   
    PrintMainMenu(error);

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Clear();
            UserMenu(false, ref users, ref accounts);
            MainMenu(false, ref users, ref accounts);
            break;
        case "2":
            Console.Clear();
            Console.WriteLine(" Odabrali ste izbornik Računi.");
            Console.ReadKey();
            // handle option 2
            MainMenu(false, ref users, ref accounts);
            break;
        case "3":
            Console.Clear();
            Console.WriteLine(" Napustili ste aplikaciju.");
            break;
        default:
            MainMenu(true, ref users, ref accounts);
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
    Console.Write("\n Vaš odabir: ");
}

static void UserMenu(bool error,ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts) // User menu controller
{
    PrintUserMenu(error);

    var option = Console.ReadLine();

    switch (option) 
    {
        case "1": // ADD NEW USER
            Console.Clear();
            var addSuccess = UserCreateDialogue(ref users, ref accounts);
            Console.Clear();
            Console.WriteLine("\n " + addSuccess + "\n\n Press any key to continue...");
            Console.ReadKey();
            break;

        case "2": // DELETE USER 
            Console.Clear();
            var deleteSuccess = DeleteUserDialogue(false, ref users, ref accounts);
            Console.Clear();
            Console.WriteLine("\n " + deleteSuccess + "\n\n Press any key to continue...");
            Console.ReadKey();
            break;

        case "3": // EDIT USER
            Console.Clear();
            var editSuccess = EditUserDialogue(ref users, ref accounts);
            Console.Clear();
            Console.WriteLine("\n " + editSuccess + "\n\n Press any key to continue...");
            Console.ReadKey();
            break;

        case "4": // PRINT USERS
            UserMenuOption(false, ref users, ref accounts);
            break;

        case "5": // EXIT TO MAIN MENU
            break;

        default:
            UserMenu(true, ref users, ref accounts);
            break;
    }

}

static void UserMenuOption(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts)
{
    Console.Clear();
    Console.WriteLine("\n IZBORNIK PREGLEDA KORISNIKA");
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
            UserMenu(false, ref users, ref accounts);
            break;

        case "b": 
            Console.Clear();
            PrintUsersOverThirty(users);
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            UserMenu(false, ref users, ref accounts);
            break;
        case "c":
            Console.Clear();
            PrintUsersInMinus(users, accounts);
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
            UserMenu(false, ref users, ref accounts);
            break;
        case "d":
            UserMenu(false, ref users, ref accounts);
            break;
        default:
            UserMenuOption(true,ref users, ref accounts);
            break;
    }
}

// User data printing

static void PrintUsersBySurname(List<Tuple<int, string, string, DateTime>> users) // Prints out the user list alphabeticaly by surname
 {
    foreach (var user in users.OrderBy(x => x.Item3))
        Console.WriteLine($" {user.Item1} - {user.Item2} - {user.Item3} - {user.Item4:dd/MM/yyyy}");
}

static void PrintUsersOverThirty(List<Tuple<int, string, string, DateTime>> users) // Prints out users in the user list that are 30+ years old
{
    foreach (var user in users.Where(x => (DateTime.Today - x.Item4) >= new TimeSpan(30*365,0,0,0)))
        Console.WriteLine($" {user.Item1} - {user.Item2} - {user.Item3} - {user.Item4:dd/MM/yyyy}");
}

static void PrintUsersInMinus(List<Tuple<int, string, string, DateTime>> users, List<Tuple<int, string, double>> accounts) // Prints out users that own accounts which are in minus
{
    var visited = new List<int>();

    foreach(var account in accounts.Where(x => x.Item3 < 0))
    {
        if(visited.Contains(account.Item1))
            continue;
        visited.Add(account.Item1);

        var user = users.FirstOrDefault(x => x.Item1 == account.Item1);

        if (user != null)
            Console.WriteLine($" {user.Item1} - {user.Item2} - {user.Item3} - {user.Item4:dd/MM/yyyy}");

    }

}

// User insertion

static string UserCreateDialogue(ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts)
{
    Console.Clear();
    Console.WriteLine("\n Upišite podatke o korisniku u formatu: ID IME PREZIME DATUM_ROĐENJA\n\n Napomena: Datum rođenja unijeti u formatu dd/MM/yyyy\n\n Unesite \"x\" za izlaz");
    Console.Write("\n Vaš unos: ");

    var input = Console.ReadLine();

    input = input == null ? "": input.Trim();

    if (input == "x")
        return "Napuštanje dodavanja korisnika...";

    try
    {
        var strings = input.Split();

        if (strings.Length > 4) // error if there are more than 4 arguments
            throw new Exception("Upisano je više od 4 argumenta!");
        
        var newUser = Tuple.Create(Int32.Parse(strings[0]), strings[1], strings[2], DateTime.Parse(strings[3])); // try to get data, if something went wrong,
        
        if(newUser.Item1 < 0)
        {
            throw new Exception("ID ne smije biti negativan.");
        }  
        if(!users.Any(x => x.Item1 == newUser.Item1)) // check if user id is taken
        {
            users.Add(newUser);
            return "Korisnik uspješno dodan!";
        }
        else
        {
            return "Uneseni ID je zauzet!";
        }
    }
    catch (Exception ex) 
    { 
        return "Pogreška pri upisu podataka!\n\n " + ex.Message; 
    }
    
}

// Delete user

static string DeleteUserDialogue(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts)
{
    Console.Clear();
    
    Console.WriteLine("\n IZBORNIK BRISANJA KORISNIKA\n");
    if (error)
        Console.WriteLine(" Pogrešan odabir!\n");
    Console.WriteLine("\ta) Brisanje po ID-u");
    Console.WriteLine("\tb) Brisanje po imenu i prezimenu");
    Console.WriteLine("\tc) Izlaz\n");
    Console.Write("Vaš odabir: ");
    
    var input = Console.ReadLine();

    return input switch
    {
        "a" => DeleteUserById(false, ref users, ref accounts),//solved
        "b" => DeleteUserByNameAndSurname(false, ref users, ref accounts),//solved
        "c" => "Izlaz iz brisanja korisnika...",
        _ => DeleteUserDialogue(true, ref users, ref accounts),
    };
}

static string DeleteUserById(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts) // solved, i think
{   
    Console.Clear();
    Console.WriteLine("\n BRISANJE KORISNIKA PO ID-u\n\n");
    if (error)
        Console.WriteLine("Pogreška pri unosu!\n");
    Console.Write("\nUnesite ID za brisanje ili x za izlaz: ");

    var input = Console.ReadLine();

    if (input == null)
        return "Greška!";

    if (input == "x")
        return "Izlaz iz brisanja korisnika.";

    try
    {
        var id = Int32.Parse(input);

        if (id < 0)
            throw new Exception("ID ne smije biti manji od 0!");

        if (users.RemoveAll(x => x.Item1 == id) == 0)
            throw new Exception("Korisnik sa unesenim ID-om ne postoji");

        return "Korisnik uspješno izbrisan!";
    }
    catch ( Exception ex)
    {
        return " Greška pri brisanju korisnika!\n " + ex.Message; 
    }
}

static string DeleteUserByNameAndSurname(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts) // solved i think
{
    Console.Clear();
    Console.WriteLine("\n BRISANJE KORISNIKA PO IMENU I PREZIMENU\n\n");
    if (error)
        Console.WriteLine("Pogreška pri unosu!\n");
    Console.Write("\nUnesite ime i prezime za brisanje ili x za izlaz: ");

    var input = Console.ReadLine();

    if (input == null) 
        return "Greška!";

    if (input == "x")
        return "Izlaz iz brisanja korisnika.";

    try
    {
        var strings = input.Split();

        if (strings.Length != 2)
            throw new Exception("Netočan format unosa!");

        if (users.RemoveAll(x => x.Item2 == strings[0] && x.Item3 == strings[1]) == 0)
            throw new Exception("Korisnik sa unesenim imenom i prezimenom ne postoji");

        return "Korisnik uspješno izbrisan!";
    }
    catch (Exception ex)
    {
        return " Greška pri brisanju korisnika!\n " + ex.Message;
    }
}

// Edit user

static string EditUserDialogue(ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double>> accounts)
{
    Console.WriteLine("\n UREĐIVANJE PODATAKA KORISNIKA\n\n");
    Console.Write(" Unesite ID korisnika čije podatke želite urediti: ");

    try
    {
        var input = Console.ReadLine();

        if (input == null)
            return "Greška!";

        var id = Int32.Parse(input);

        if (id < 0)
            throw new Exception("ID ne smije biti negativan!");

        if (!users.Any(x => x.Item1 == id))
            throw new Exception("Korisnik ne postoji!");

        Console.WriteLine("\n Podaci odabranog korisnika: " + users.Find(x => x.Item1 == id));
        Console.WriteLine("\n Upišite podatke o korisniku u formatu: IME PREZIME DATUM_ROĐENJA\n\n Napomena: Datum rođenja unijeti u formatu dd/MM/yyyy\n\n Unesite \"x\" za izlaz");
        Console.Write("\n Vaš unos: ");

        var data = Console.ReadLine();

        if (data == null)
            return "Greška!";

        if (data == "x")
            return "Izlaz iz uređivanja podataka korisnika...";

        var strings = data.Split();

        if (strings.Length > 3) // error if there are more than 4 arguments
            throw new Exception("Upisano je više od 3 argumenta!");

        users[users.FindIndex(x=> x.Item1 == id)] = Tuple.Create(id, strings[0], strings[1], DateTime.Parse(strings[2]));

        return "Podaci uspješno ažurirani!";

    }
    catch
    {
        return "Pogreška pri upisu podataka!\n\n ";
    }
}


// Start of program

MainMenu(false, ref users, ref accounts);
