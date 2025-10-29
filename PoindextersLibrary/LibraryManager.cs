using System.Text.Json;

namespace PoindextersLibrary;

public static class LibraryManager
{
    public static List<Book> Books { get; set; } = [];
    public static int IdCounter { get; set; } = 0;
    
    public const int DefaultQueryLimit = 20;

    public static bool bBooksMenuIsShown = false;

    public static void PrintBooks() => Books.ForEach(book => Console.WriteLine($"{book.Id}. {book.Name}"));

    public static void LoanBooks()
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