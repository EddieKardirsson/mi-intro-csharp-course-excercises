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
        LibraryManager.Books.ForEach(book => Console.WriteLine(book.Name.ToString()));
    }
}