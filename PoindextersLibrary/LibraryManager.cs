using System.Text.Json;

namespace PoindextersLibrary;

public static class LibraryManager
{
    private static List<Book> Books { get; set; } = [];
    private static List<User> Users { get; set; } = [new User("mikaelmikael", "Mikael", "Mikael", "mikael.mikael@fakemail.com")];
    
    //public static Dictionary<User, List<Book>> ActiveLoans { get; set; } = new Dictionary<User, List<Book>>();

    private static Dictionary<Guid, List<Book>> ActiveLoans { get; } = new();
    private static User? LoggedInUser { get; set; }
    public static int IdCounter { get; set; } = 0;
    
    public const int DefaultQueryLimit = 20;

    public static bool bBooksMenuIsShown = false;
    
    public static void PrintBooks() => Books.ForEach(book => Console.WriteLine($"{book.Id}. {book.Name}"));
   
    // Main book loaning logic
    public static void LoanBooks()
    {
        Console.WriteLine("Enter book's ID to loan or press B to exit: ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "b")
        {
            bBooksMenuIsShown = false;
            return;
        }

        if (!int.TryParse(input, out var index))
        {
            Console.WriteLine("Invalid input. Please enter a valid book ID or 'B' to exit.");
            return;
        }

        var bookToLoan = Books.Find(book => book.Id == index);
        if (bookToLoan is null)
        {
            Console.WriteLine("Book not found. Please try again.");
            return;
        }

        if (TryAddLoan(LoggedInUser, bookToLoan))
        {
            Console.WriteLine($"You have successfully loaned '{bookToLoan.Name}'. It is due on {bookToLoan.DueDate!.Value.ToShortDateString()}.");
        }
    }

    // Helper method to add a loan to the user's active loans'
    private static bool TryAddLoan(User? user, Book book)
    {
        if (user is null)
        {
            Console.WriteLine("No user is logged in.");
            return false;
        }

        if (book.IsLoaned)
        {
            Console.WriteLine($"Sorry, '{book.Name}' is already loaned out. It is due back on {book.DueDate?.ToShortDateString()}.");
            return false;
        }

        Guid userId = user.GetUserGuid(); // assumes User has an ID of type Guid
        if (!ActiveLoans.TryGetValue(userId, out var loans))
        {
            loans = new List<Book>();
            ActiveLoans[userId] = loans;
        }

        // Avoid duplicate entries for the same book (edge case identified)
        if (loans.Any(b => b.Id == book.Id))
        {
            Console.WriteLine("This book is already in the user's active loans.");
            return false;
        }

        book.IsLoaned = true;
        book.DueDate = DateTime.Now.AddDays(30);
        loans.Add(book);
        return true;
    }
    
    /*public static void LoanBooks()
    {
        Console.WriteLine("Enter book's ID to loan or press B to exit: ");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "b")
        {
            bBooksMenuIsShown = false;
        }
        else
        {
            int index;
            if (int.TryParse(input, out index))
            {
                Book? bookToLoan = Books.Find(book => book.Id == index);
                if (bookToLoan != null)
                {
                    if (!bookToLoan.IsLoaned)
                    {
                        bookToLoan.IsLoaned = true;
                        bookToLoan.DueDate = DateTime.Now.AddDays(30);
                        // TODO: add loan to database, input credentials, etc.
                        ActiveLoans.Add(LoggedInUser, [bookToLoan]);
                        Console.WriteLine($"You have successfully loaned '{bookToLoan.Name}'. It is due on {bookToLoan.DueDate.Value.ToShortDateString()}.");
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, '{bookToLoan.Name}' is already loaned out. It is due back on {bookToLoan.DueDate?.ToShortDateString()}.");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid book ID or 'B' to exit.");
            }
        }
    }*/

    public static void ShowActiveLoans()
    {
        if (LoggedInUser == null)
        {
            Console.WriteLine("\nUser not logged in. Please login to view active loans.");
            Console.ReadKey(false);
            return;
        }
        Console.Clear();
        Console.WriteLine("Active loans:");
        if (!ActiveLoans.ContainsKey(LoggedInUser.GetUserGuid()))
        {
            Console.WriteLine("No active loans found.");
            Console.ReadKey(false);
            return;
        }
        ActiveLoans[LoggedInUser.GetUserGuid()].ForEach(
            book => Console.WriteLine($"{book.Id}. {book.Name} - Due back on {book.DueDate?.ToShortDateString()}")
        );
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine($"To return a book, write the book's ID and press Enter, or type B to exit.");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "b") return;
        if (int.TryParse(input, out var bookId))
        {
            ReturnBook(bookId);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid book ID.");
        }
    }

    private static void ReturnBook(int bookId)
    {
        Book? bookToReturn = ActiveLoans[LoggedInUser.GetUserGuid()].Find(book => book.Id == bookId);
        if (bookToReturn != null)
        {
            bookToReturn.IsLoaned = false;
            bookToReturn.DueDate = null;
            ActiveLoans[LoggedInUser.GetUserGuid()].Remove(bookToReturn);
            Console.WriteLine($"You have successfully returned '{bookToReturn.Name}'.");
            if(ActiveLoans[LoggedInUser.GetUserGuid()].Count == 0)
                ActiveLoans.Remove(LoggedInUser.GetUserGuid());
        }
        else
        {
            Console.WriteLine("Book not found in your active loans.");
        }
    }

    public static void SearchBooks()
    {
        throw new NotImplementedException();
    }

    public static void Login()
    {
        Console.Clear();
        Console.WriteLine("Please login to continue.");
        Console.Write("Enter User Name: ");
        string? userName = Console.ReadLine();
        User? user = Users.Find(u => u.UserName == userName);
        if (user != null)
        {
            LoggedInUser = user;
            Console.WriteLine($"Welcome {LoggedInUser.UserName}!");
        }
        else
            Console.WriteLine("User not found. Please try again.");
        
        Console.ReadKey(false);
    }

    public static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("User Registration");
        Console.Write("Enter First Name : ");
        string? firstName = Console.ReadLine();
        Console.Write("Enter Last Name : ");
        string? lastName = Console.ReadLine();
        Console.Write($"Enter User Name: ");
        string? userName = Console.ReadLine();
        Console.Write("Enter Email : ");
        string? email = Console.ReadLine();
        
        User user = new User(userName, firstName, lastName, email);
        Users.Add(user);
        LoggedInUser = user;
        
        Console.WriteLine($"User {userName} Registered!");
        Console.ReadKey(false);
    }
    
    public static async void FetchBooks(string query, int limit = DefaultQueryLimit)
    {
        Console.WriteLine("Fetching books...");
        
        // check if the query is already stored (get cached data)
        var booksFromFile = GetBooksFromFile(query, limit);
        if (booksFromFile != null)
        {
            Console.WriteLine("Cached data found. Loading from file...");
            booksFromFile.ForEach(book => Books.Add(book));
            Console.WriteLine("Loading Done...");
            return;
        }
        
        Console.WriteLine("No cached data found. Fetching from OpenLibrary API...");
        List<Book> response = await OpenLibraryClient.SearchBooksAsync(query, limit);
        response.ForEach(book => Books.Add(book));
        
        // store the response in a file (caching)
        PrintBooksToFile(response, query, limit);
        
        Console.WriteLine("Fetching Done...");
    }

    private static void PrintBooksToFile(List<Book> response, string query, int limit = DefaultQueryLimit)
    {
        string filePath = GenerateFilePath(query, limit);
        using StreamWriter writer = new StreamWriter($"{filePath}");
        
        writer.WriteLine("[");
        for (int i = 0; i < response.Count; i++)
        {
            Book book = response[i];
            string bookJson = JsonSerializer.Serialize(book);
            writer.WriteLine(bookJson);
            if (i < response.Count - 1)
            {
                writer.WriteLine(",");
            }
            else
            {
                writer.WriteLine("]");
            }
        }
        Console.WriteLine("DEBUG: File written.");
        writer.Flush();
        writer.Close();
    }
    
    private static List<Book>? GetBooksFromFile(string query, int limit = DefaultQueryLimit)
    {
        string filePath = GenerateFilePath(query, limit);
        if (!File.Exists(filePath))
        {
            Console.WriteLine("DEBUG: File not found.");
            return null;
        }
        
        using StreamReader reader = new StreamReader($"{filePath}");
        string json = reader.ReadToEnd();
        
        // no need for options, since we know the data is valid due to the file written by PrintBooksToFile()
        List<Book>? books = JsonSerializer.Deserialize<List<Book>>(json);
        if (books == null)
        {
            Console.WriteLine("DEBUG: Error reading file.");
            return null;
        }
        
        reader.Close();
        return books;
    }

    private static string GenerateFilePath(string query, int limit)
    {
        string fileName = $"{query}_{limit}.json";
        
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        Directory.CreateDirectory(dataDir); // safe if it already exists
        
        string filePath = Path.Combine(dataDir, fileName);
        Console.WriteLine($"DEBUG: Generated file path: {filePath}");
        return filePath;
    }
    
    public static readonly string[] Keywords = ["fantasy", "horror", "romance", "science fiction", "drama"];
}