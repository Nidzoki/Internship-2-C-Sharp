
// test user list

using System.Reflection.Metadata.Ecma335;

var users = new List<Tuple<int, string, string, DateTime>>() // USER -> Tuple(int ID, string Name, string Surname, DateTime birthday)
{
    Tuple.Create(1, "A"/*nte"*/, "A"/*ntić"*/, new DateTime(2001,09,21)),
    Tuple.Create(2, "Pero", "Perić", new DateTime(1999,10,22)),
    Tuple.Create(3, "Karlo", "Ima vise od 30", new DateTime(1973,12,24))
};


//test account list

var accounts = new List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>>() // ACCOUNT -> Tuple(int UserID, string accountType, double balance, List of transactions(Tuple(id transactionID, double Value, string description, string Type, string category, DateTime time)))
{
    Tuple.Create(1, "žiro", 0.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(1, "tekući", 2.0, new List<Tuple<int, double, string, string, string, DateTime>>()
    {
        Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now),
        Tuple.Create(1, 8.1, "standardna transakcija", "prihod", "plaća", DateTime.Now),
        Tuple.Create(1, 4.0, "standardna transakcija", "rashod", "prijevoz", DateTime.Now),
        Tuple.Create(1, 11.5, "standardna transakcija", "prihod", "plaća", DateTime.Now),
        Tuple.Create(1, 20.1, "standardna transakcija", "prihod", "plaća", DateTime.Now)
    }),
    Tuple.Create(1, "prepaid", -200.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(2, "žiro", -1.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(2, "tekući", 1.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(2, "prepaid", 0.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(3, "žiro", 2.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(3, "tekući", -200.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)}),
    Tuple.Create(3, "prepaid", -1.0, new List<Tuple<int, double, string, string, string, DateTime>>(){Tuple.Create(1, 4.90, "standardna transakcija", "prihod", "plaća", DateTime.Now)})
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

static void MainMenu(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts) // Main menu controller
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
            var accountSuccess = AccountMenuController(ref users, ref accounts);
            Console.Clear();
            Console.WriteLine(accountSuccess);
            Console.Write(" Press any key to continue...");
            Console.ReadKey();
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
    Console.WriteLine("\n IZBORNIK KORISNICI\n\n 1 - Unos novog korisnika\n 2 - Brisanje korisnika\n 3 - Uređivanje korisnika\n 4 - Pregled korisnika\n 5 - Natrag");
    Console.Write("\n Vaš odabir: ");
}

static void UserMenu(bool error,ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts) // User menu controller
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

static void UserMenuOption(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts)
{
    Console.Clear();
    Console.WriteLine("\n IZBORNIK PREGLEDA KORISNIKA");
    if (error) 
        Console.WriteLine("\n Neispravan odabir! ");
    Console.WriteLine("\n a) ispis svih korisnika abecedno po prezimenu");
    Console.WriteLine(" b) ispis svih onih koji imaju više od 30 godina");
    Console.WriteLine(" c) ispis svih onih koji imaju barem jedan račun u minusu");
    Console.WriteLine(" d) izlaz");

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

static void PrintUsersInMinus(List<Tuple<int, string, string, DateTime>> users, List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts) // Prints out users that own accounts which are in minus
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

static string UserCreateDialogue(ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts)
{
    Console.Clear();
    Console.WriteLine("\n Upišite podatke o korisniku u formatu: IME PREZIME DATUM_ROĐENJA\n\n Napomena: Datum rođenja unijeti u formatu dd/MM/yyyy\n\n Unesite \"x\" za izlaz");
    Console.Write("\n Vaš unos: ");

    var input = Console.ReadLine();

    input = input == null ? "": input.Trim();

    if (input == "x")
        return "Napuštanje dodavanja korisnika...";

    try
    {
        var strings = input.Split();

        if (strings.Length > 3) // error if there are more than 3 arguments
            throw new Exception("Upisano je više od 3 argumenta!");
        
        var newUser = Tuple.Create(users.Count > 0 ? users.Select(x => x.Item1).Max() + 1 : 1, strings[0], strings[1], DateTime.Parse(strings[2])); // try to get data, if something went wrong, error will be thrown
        
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

static string DeleteUserDialogue(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts)
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

static string DeleteUserById(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts) // solved, i think
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

static string DeleteUserByNameAndSurname(bool error, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts) // solved i think
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

static string EditUserDialogue(ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts)
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


// Account menu

static void PrintTransactionOptions()
{
    Console.WriteLine("\n OPCIJE TRANSAKCIJA\n\n 1 - Unos nove transakcije\n 2 - Brisanje transakcije\n 3 - Uređivanje transakcije\n 4 - Pregled transakcija\n 5 - Financijsko izvješće\n 0 - Izlaz");
    Console.Write("\n\n Vaš odabir: ");
}


static void PrintTransactionViewOptions()
{
    Console.WriteLine("\n a) sve transakcije kako su spremljene\n b) sve transakcije sortirane po iznosu uzlazno\n c) sve transakcije sortirane po iznosu silazno");
    Console.WriteLine(" d) sve transakcije sortirane po opisu abecedno\n e) sve transakcije sortirane po datumu uzlazno\n f) sve transakcije sortirane po datumu silazno");
    Console.WriteLine(" g) svi prihodi\n h) svi rashodi\n i) sve transakcije za odabranu kategoriju\n j) sve transakcije za odabrani tip i kategoriju");
    Console.Write("\n Vaš odabir: ");
}

static string AccountMenuController(ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts)
{
    Console.Clear();
    Console.WriteLine("\n ODABIR KORISNIKA\n");
    Console.Write(" Unesite x za izlaz ili ime i prezime korisnika za pregled računa: ");
    
    var userNameAndSurname = Console.ReadLine();

    if (userNameAndSurname == null)
        return "Greška!";

    if (userNameAndSurname == "x")
        return "Izlaz iz pregleda računa...";

    var strings = userNameAndSurname.Split();

    if (strings.Length != 2)
        return "Netočan format unesenih podataka!";

    if (!users.Any(x => x.Item2 == strings[0] && x.Item3 == strings[1]))
        return "Korisnik ne postoji!";

    var userData = users.FirstOrDefault(x => x.Item2 == strings[0] && x.Item3 == strings[1]);

    if (userData == null) 
        return "Greška!";

    Console.WriteLine($"\n Računi korisnika: {userData.Item2 + " " + userData.Item3}: ");



    foreach (var account in accounts.Where(x => x.Item1 == userData.Item1))
    {
        Console.WriteLine($"\t Tip: {account.Item2} Stanje: {account.Item3}" );
    }
    Console.WriteLine("\n Odaberite kojim računom želite upravljati:\n\ta) tekući\n\tb) žiro\n\tc) prepaid\n\td) izlaz\n");
    Console.Write(" Vaš odabir: ");

    var accountType = Console.ReadLine();

    if (accountType == null)
        return "Greška";

    switch (accountType)
    {
        case "a":
            var accountDataA = accounts.Find(x => x.Item1 == userData.Item1 && x.Item2 == "tekući");

            if (accountDataA == null)
                return "Greška!"; 

            Console.Clear();
            Console.WriteLine($"\n PREGLED ODABRANOG RAČUNA\n\n Korisnik: {userData.Item2 + " " + userData.Item3}\n Tip računa: {accountDataA.Item2}\n Stanje računa: {accountDataA.Item3} EUR\n");
            PrintTransactionOptions();

            var optionA = Console.ReadLine();

            if (optionA == null) return "Greška";
            return HandleTransactionOptions(optionA, accountDataA, ref users, ref accounts);
        case "b":
            var accountDataB = accounts.Find(x => x.Item1 == userData.Item1 && x.Item2 == "žiro");
            if (accountDataB == null)
                return "Greška!";
            Console.Clear();
            Console.WriteLine($"\n PREGLED ODABRANOG RAČUNA\n\n Korisnik: {userData.Item2 + " " + userData.Item3}\n Tip računa: {accountDataB.Item2}\n Stanje računa: {accountDataB.Item3} EUR\n");
            PrintTransactionOptions();
            break;
        case "c":
            var accountDataC = accounts.Find(x => x.Item1 == userData.Item1 && x.Item2 == "prepaid");
            if (accountDataC == null)
                return "Greška!";
            Console.Clear();
            Console.WriteLine($"\n PREGLED ODABRANOG RAČUNA\n\n Korisnik: {userData.Item2 + " " + userData.Item3}\n Tip računa: {accountDataC.Item2} \n Stanje računa:  {accountDataC.Item3} EUR\n");
            PrintTransactionOptions();

            break;
        case "d":
            return "Izlaz iz odabira računa...";
        default:
            return "Greška pri upisu";
    }

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();
    return " Izlaz iz pregleda\n";
}

static string HandleTransactionOptions(string option, Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>> accountData, ref List<Tuple<int, string, string, DateTime>> users, ref List<Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>>> accounts)
{
    switch (option)
    {   
        case "1": throw new NotImplementedException(); // new transaction 
        case "2": throw new NotImplementedException(); // delete transaction
        case "3": throw new NotImplementedException(); // edit transaction
        case "4":  // view transactions
            Console.Clear();
            PrintTransactionViewOptions();
            var viewType = Console.ReadLine();
            if (viewType == null) return " Greška!";
            var viewResult = viewType switch
            {
                "a" => PrintAllTransactions(accountData.Item4),
                "b" => PrintAllTransactionsAscending(accountData.Item4),
                "c" => PrintAllTransactionsDescending(accountData.Item4),
                "d" => PrintAllTransactionsByDescription(accountData.Item4),
                "e" => PrintAllTransactionsByDateAscending(accountData.Item4),
                "f" => PrintAllTransactionsByDateDescending(accountData.Item4),
                "g" => PrintAllIncomes(accountData.Item4),
                "h" => PrintAllOutcomes(accountData.Item4),
                "i" => PrintAllTransactionsSelectedCategory(accountData.Item4),
                "j" => PrintAllTransactionsSelectedCategoryAndType(accountData.Item4),
                _ => " Izlaz"
            };
            return viewResult;
        case "5":  // financial report
            Console.Clear();
            PrintReportOptions();
            var reportType = Console.ReadLine();
            if (reportType == null) return " Greška!";
            var reportResult = reportType switch
            {
                "a" => PrintAccountBallance(accountData),
                "b" => PrintTransactionCount(accountData.Item4),
                "c" => PrintIncomesAndOutcomesOfSelectedMonth(accountData.Item4),
                "d" => PrintCategoryPercentage(accountData.Item4),
                "e" => PrintAverageForSelectedMonth(accountData.Item4),
                "f" => PrintAverageForSelectedCategory(accountData.Item4),
                _ => " Izlaz"
            };
            return reportResult;
        case "0": return " Izlaz";
        default: return " Greška!";
    }
}

static string PrintAverageForSelectedCategory(List<Tuple<int, double, string, string, string, DateTime>> transactionList) // solved
{
    Console.Clear();
    Console.WriteLine("\n Odaberite kategoriju:\n\ta) plaća\n\tb) honorar\n\tc) poklon\n\td) hrana\n\te) prijevoz\n\tf) sport");

    Console.Write("\n\n Vaš odabir: ");

    var option = Console.ReadLine() switch
    {
        "a" => "plaća",
        "b" => "honorar",
        "c" => "poklon",
        "d" => "hrana",
        "e" => "prijevoz",
        "f" => "sport",
        _ => " Izlaz"
    };

    if (option == " Izlaz")
        return option;

    return "\n Prosjek transakcija odabrane kategorije iznosi: " + transactionList.Where(x => x.Item5 == option).Select(x => x.Item2).Average().ToString() + "\n";

}

static string PrintAverageForSelectedMonth(List<Tuple<int, double, string, string, string, DateTime>> transactionList) // solved
{
    Console.Clear();
    Console.Write("\n Upišite mjesec i godinu u formatu mm yyyy:");

    var input = Console.ReadLine();

    if (input == null)
        return "Greška!";
    var month = 0;
    var year = 0;
    try
    {
        var splitted = input.Split();
        month = int.Parse(splitted[0]);
        year = int.Parse(splitted[1]);

        if (month < 1 || month > 12 || year < 1 || year > DateTime.UtcNow.Year)
            return "\n Greška!\n Mjesec koji ste odabrali nije važeći!\n";
    }
    catch
    {
        return "Greška!";
    }


    var selectedTransactions = transactionList.Where(x => x.Item6.Month == month && x.Item6.Year == year);

    return selectedTransactions.Any() ?$"\n Prosjek za mjesec {month}/{year}: {transactionList.Where(x => x.Item6.Month == month && x.Item6.Year == year).Select(x => x.Item2).Average().ToString()}\n" : $"\n Za mjesec {month}/{year} nema nikakvih transakcija.\n";
}

static string PrintCategoryPercentage(List<Tuple<int, double, string, string, string, DateTime>> transactionList) // solving now
{
    Console.Clear();
    Console.Write("\n Upišite mjesec i godinu u formatu mm yyyy:");

    var input = Console.ReadLine();

    if (input == null)
        return "Greška!";
    var month = 0;
    var year = 0;
    try
    {
        var splitted = input.Split();
        month = int.Parse(splitted[0]);
        year = int.Parse(splitted[1]);

        if (month < 1 || month > 12 || year < 1 || year > DateTime.UtcNow.Year)
            return "\n Greška!\n Mjesec koji ste odabrali nije važeći!\n";
    }
    catch
    {
        return "Greška!";
    }


    var selectedTransactions = transactionList.Where(x => x.Item6.Month == month && x.Item6.Year == year);

    return selectedTransactions.Any() ? $"\n Prosjek za mjesec {month}/{year}: {transactionList.Where(x => x.Item6.Month == month && x.Item6.Year == year).Select(x => x.Item2).Average().ToString()}\n" : $"\n Za mjesec {month}/{year} nema nikakvih transakcija.\n";
}

static string PrintIncomesAndOutcomesOfSelectedMonth(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    throw new NotImplementedException();
}

static string PrintTransactionCount(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    throw new NotImplementedException();
}

static string PrintAccountBallance(Tuple<int, string, double, List<Tuple<int, double, string, string, string, DateTime>>> accountData)
{
    throw new NotImplementedException();
}


// Transactions printout methods

static string PrintAllTransactionsSelectedCategoryAndType(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n Odaberite kategoriju:\n\ta) plaća\n\tb) honorar\n\tc) poklon\n\td) hrana\n\te) prijevoz\n\tf) sport");

    Console.Write("\n\n Vaš odabir: ");

    var categoryOption = Console.ReadLine() switch
    {
        "a" => "plaća",
        "b" => "honorar",
        "c" => "poklon",
        "d" => "hrana",
        "e" => "prijevoz",
        "f" => "sport",
        _ => " Izlaz"
    };

    if (categoryOption == " Izlaz")
        return categoryOption;

    Console.Clear();
    Console.WriteLine("\n Odaberite tip:\n\ta) prihod\n\tb) rashod");

    Console.Write("\n\n Vaš odabir: ");

    var typeOption = Console.ReadLine() switch
    {
        "a" => "prihod",
        "b" => "rashod",
        _ => " Izlaz"
    };

    if (typeOption == " Izlaz")
        return typeOption;

    foreach (var transaction in transactionList.Where(x => x.Item5 == categoryOption && x.Item4 == typeOption))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");

    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactionsSelectedCategory(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n Odaberite kategoriju:\n\ta) plaća\n\tb) honorar\n\tc) poklon\n\td) hrana\n\te) prijevoz\n\tf) sport");

    Console.Write("\n\n Vaš odabir: ");

    var option = Console.ReadLine() switch
    {
        "a" => "plaća",
        "b" => "honorar",
        "c" => "poklon",
        "d" => "hrana",
        "e" => "prijevoz",
        "f" => "sport",
        _ => " Izlaz"
    };

    if (option == " Izlaz")
        return option;

    foreach (var transaction in transactionList.Where(x => x.Item5 == option))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");

    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllOutcomes(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO DATUMU SILAZNO\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.Where(x => x.Item4 == "rashod"))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllIncomes(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO DATUMU SILAZNO\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.Where(x => x.Item4 == "prihod"))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactionsByDateDescending(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO DATUMU SILAZNO\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.OrderByDescending(x => x.Item6))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactionsByDateAscending(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO DATUMU UZLAZNO\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.OrderBy(x => x.Item6))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactionsByDescription(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO OPISU\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.OrderBy(x => x.Item3))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactionsDescending(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO IZNOSU SILAZNO\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.OrderByDescending(x => x.Item2))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactionsAscending(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE SORTIRANE PO IZNOSU UZLAZNO\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList.OrderBy(x => x.Item2))
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

static string PrintAllTransactions(List<Tuple<int, double, string, string, string, DateTime>> transactionList)
{
    Console.Clear();
    Console.WriteLine("\n SVE TRANSAKCIJE\n\n Tip - Iznos - Opis - Kategorija - Datum\n");

    foreach (var transaction in transactionList)
        Console.WriteLine($" {transaction.Item4} - {transaction.Item2} - {transaction.Item3} - {transaction.Item5} - {transaction.Item6}");

    Console.WriteLine("\n Press any key to continue...");
    Console.ReadKey();

    return " Izlaz";
}

// Financial report


static void PrintReportOptions()
{
    Console.WriteLine("\n OPCIJE FINANCIJSKOG IZVJEŠĆA\n\n a) trenutno stanje računa\n b) broj ukupnih transakcija\n c) ukupan iznos prihoda i rashoda za odabrani mjesec i godinu");
    Console.WriteLine(" d) postotak udjela rashoda za odabranu kategoriju\n e) prosječni iznos transakcije za odabrani mjesec i godinu\n f) prosječni iznos transakcije za odabranu kategoriju");
    Console.Write("\n Vaš odabir: ");
}

// Start of program

MainMenu(false, ref users, ref accounts);
