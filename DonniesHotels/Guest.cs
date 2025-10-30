namespace DonniesHotels;

public class Guest
{
    public Guid GuestId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public Room? BookedRoom { get; set; }
    public Guid? BookingId { get; set; }
    
    public Guest(string name, int age, string email)
    {
        GuestId = Guid.NewGuid();
        Name = name;
        Age = age;
        Email = email;
    }
}

