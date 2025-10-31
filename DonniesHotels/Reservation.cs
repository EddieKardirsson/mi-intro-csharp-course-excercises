namespace DonniesHotels;

public class Reservation
{
    public Guid ReservationId { get; }
    public Room Room { get; set; }
    public Guest Guest { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Reservation(Room room, Guest guest, DateTime startDate, DateTime endDate)
    {
        ReservationId = Guid.NewGuid();
        Room = room;
        Guest = guest;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static bool IsOverlapping(Reservation res1, Reservation res2)
    {
        return res1.Room.RoomNumber == res2.Room.RoomNumber &&
               res1.StartDate < res2.EndDate &&
               res2.StartDate < res1.EndDate;
    }
}