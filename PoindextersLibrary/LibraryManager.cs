using System.Text.Json;

namespace PoindextersLibrary;

public static class LibraryManager
{
    public static List<Book> Books { get; set; } = [];
    
    public const int DefaultQueryLimit = 20;

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