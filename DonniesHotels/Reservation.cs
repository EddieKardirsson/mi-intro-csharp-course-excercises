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
}