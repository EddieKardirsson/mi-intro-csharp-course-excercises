namespace DonniesHotels;

public class Guest
{
    public Guid GuestId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public Room? BookedRoom { get; set; }
    public Guid? BookingId { get; set; }

    private bool _isAdmin;
    
    public Guest(string name, int age, string email, bool isAdmin = false)
    {
        GuestId = Guid.NewGuid();
        Name = name;
        Age = age;
        Email = email;
        _isAdmin = isAdmin;
    }
    
    public bool IsAdmin => _isAdmin;
    public void SetAdmin() => _isAdmin = true;
}

