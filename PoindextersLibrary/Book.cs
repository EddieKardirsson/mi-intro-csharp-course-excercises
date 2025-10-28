namespace PoindextersLibrary;

public class Book
{
    private int Id { get; }
    public string Name { get; } = string.Empty;
    public string Author { get; } = string.Empty;
    public string Publisher { get; } = string.Empty;
    public int Year { get; }
    // ReSharper disable once InconsistentNaming
    public string ISBN { get; }
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