using System.ComponentModel.DataAnnotations;

namespace PoindextersLibrary;

public class User
{
    private Guid Id { get; }
    public string UserName { get; set; }
    private string FirstName { get; set; }
    private string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    public List<Book> BorrowedBooks = [];
    
    public User(string userName, string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public Guid GetUserGuid() => Id;
}