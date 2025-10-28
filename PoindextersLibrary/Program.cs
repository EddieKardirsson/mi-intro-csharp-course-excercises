namespace PoindextersLibrary;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorTop++;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Hello, World!");
        LibraryManager.FetchBooks();
        Console.ReadKey(false);
        LibraryManager.Books.ForEach(book => Console.WriteLine(book.Name.ToString()));
    }
}