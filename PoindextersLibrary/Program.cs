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
        Console.WriteLine("3. Exit");
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
                break;
            case '3':
                Console.WriteLine("\nGoodbye!");
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
}