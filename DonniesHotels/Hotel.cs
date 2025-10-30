namespace DonniesHotels;

public class Hotel
{
    public List<Room> HotelRooms { get; } = new List<Room>();
    public Dictionary<Room, Guest?> Reservations { get; } = new Dictionary<Room, Guest?>();
    public static List<Guest> Guests { get; } = new List<Guest>();  // for all hotels
    public Guest? LoggedInGuest { get; set; } = null;
    public string Location { get; }
    public int Floors { get; }
    public int MaxRoomsPerFloor { get; }
    public float MaxAreaPerFloor { get; set; }
    public static decimal TotalRevenue { get; set; } = 0m;  // for all hotels

    public Hotel(string location, int floors, int maxRoomsPerFloor, float maxAreaPerFloor)
    {
        Location = location;
        Floors = floors;
        MaxRoomsPerFloor = maxRoomsPerFloor;
        MaxAreaPerFloor = maxAreaPerFloor;
    }

    // TODO: Register Guest
    public static void RegisterGuest(string name, int age, string email)
    {
        Guests.Add(new Guest(name, age, email));
        Console.WriteLine($"Guest {name} Registered!");
    }
    
    // TODO: Book Room
    
    // TODO: Check Availability
    
    // TODO: Cancel Reservation
    
    // TODO: Guest Login
    
    // TODO: Guest Logout
    
    // TODO: Calculate Revenue
}