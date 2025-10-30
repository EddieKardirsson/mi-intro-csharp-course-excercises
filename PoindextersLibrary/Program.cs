namespace PoindextersLibrary;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorTop++;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Welcome to Poindexter's Library!");
        LibraryManager.FetchBooks(LibraryManager.Keywords[0]);
        Console.ReadKey(false);
        //LibraryManager.Books.ForEach(book => Console.WriteLine(book.Name.ToString()));
        while (true)
            MainMenu();
    }

    private static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("\nMain Menu");
        Console.WriteLine("1. View Books");
        Console.WriteLine("2. Search Books");
        Console.WriteLine("3. Register User");
        Console.WriteLine("4. Login");
        Console.WriteLine("5. Borrowed Books"); // Here you hand in the books too
        Console.WriteLine("6. Logout");
        Console.WriteLine("Q. Exit");
        Console.Write("Select an option: ");
        
        char input = Console.ReadKey().KeyChar;

        switch (input)
        {
            case '1':
                LibraryManager.bBooksMenuIsShown = true;
                while (LibraryManager.bBooksMenuIsShown)
                {
                    Console.Clear();
                    Console.WriteLine();
                    LibraryManager.PrintBooks();
                    TODO: LibraryManager.LoanBooks();
                    Console.ReadKey();    
                }
                break;
            case '2':
                // TODO: LibraryManager.SearchBooks();
                break;
            case '3':
                LibraryManager.RegisterUser();
                break;
            case '4':
                LibraryManager.Login();
                break;
            case '5':
                LibraryManager.ShowActiveLoans();
                break;
            case '6':
                // TODO: LibraryManager.Logout();
                break;
            case 'q':
            case 'Q':
                Console.WriteLine("\nGoodbye!");
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
}