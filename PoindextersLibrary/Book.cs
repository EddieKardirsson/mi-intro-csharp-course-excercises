namespace PoindextersLibrary;

public class Book
{
    private int Id { get; }
    public string Name { get; } = string.Empty;
    public string Author { get; } = string.Empty;
    public string Publisher { get; } = string.Empty;
    public int Year { get; }
    // ReSharper disable once InconsistentNaming
    public string ISBN { get; } = string.Empty; // OpenLibrary doesn't seem to have a proper ISBN property, but I will keep it there in case I find a better and more consistent API. At Least OpenLibrary is free and is very generous with API calls, even if it's very inconsistent.
    public bool IsLoaned { get; set; }
    public DateTime? DueDate { get; set; }
    
    private const int MaxLoanDays = 30;
    
    public Book(string name, string author, string publisher, int year, string isbn)
    {
        Name = name;
        Author = author;
        Publisher = publisher;
        Year = year;
        ISBN = isbn;
    }
    
    
}