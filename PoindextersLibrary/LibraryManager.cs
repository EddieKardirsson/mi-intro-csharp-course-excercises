namespace PoindextersLibrary;

public static class LibraryManager
{
    public static List<Book> Books { get; set; } = [];

    public static async void FetchBooks()
    {
        Console.WriteLine("Fetching books...");
        var response = await OpenLibraryClient.SearchBooksAsync("Fantasy");
        response.ForEach(book => Books.Add(book));
        Console.WriteLine("Fetching Done.");
    }
}