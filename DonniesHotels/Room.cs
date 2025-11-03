namespace DonniesHotels;

public enum RoomType
{
    Single, // 1 person
    Double, // 2 people
    Family, // 4 people
    Suite
}

public class Room
{
    public RoomType Type { get; }
    public decimal Price { get; } // per night
    public int Floor { get; set; }
    public int RoomNumber { get; set; }
    public float Area { get; set; } // in m2
    public bool IsBooked { get; set; } = false;
    public Guid? BookingId { get; set; }
    
    public Room(RoomType type, int floor, decimal price, int roomNumber, float area)
    {
        Type = type;
        Floor = floor;
        Price = price;
        RoomNumber = roomNumber;
        Area = area;
    }
    
    
    
}

