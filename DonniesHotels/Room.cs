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
    public int Price { get; } // per night
    public int RoomNumber { get; }
    public float Area { get; set; } // in m2
    public bool IsBooked { get; set; } = false;
    public Guid? BookingId { get; set; }
    
    public Room(RoomType type, int price, int roomNumber, float area)
    {
        Type = type;
        Price = price;
        RoomNumber = roomNumber;
        Area = area;
    }
}

