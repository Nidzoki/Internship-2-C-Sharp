
using System.Linq.Expressions;
using System.Threading.Channels;

static int PrintMainMenu(bool error)
{
    Console.Clear();
    if (error)
        Console.WriteLine("Pogrešno upisan odabir!");

    Console.WriteLine("\n GLAVNI IZBORNIK\n\n 1 - Korisnici\n 2 - Računi\n 3 - Izlaz iz aplikacije");
    Console.Write("\n\n Vaš odabir: ");
    return 0;
}

static int MainMenu(bool error)
{
    PrintMainMenu(error);

    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            Console.Clear();
            Console.WriteLine("Odabrali ste izbornik Korisnici.");
            Console.ReadKey();
            // handle option 1
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


MainMenu(false);