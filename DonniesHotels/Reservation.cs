namespace DonniesHotels;

public class Reservation
{
    public Guid ReservationId { get; }
    public Room Room { get; set; }
    public Guest Guest { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    private int _numberOfNights; 
    
    public Reservation(Room room, Guest guest, DateTime startDate, DateTime endDate)
    {
        ReservationId = Guid.NewGuid();
        Room = room;
        Guest = guest;
        StartDate = startDate;
        EndDate = endDate;
        _numberOfNights = (int) (endDate - startDate).TotalDays;
    }

    public static bool IsOverlapping(Reservation res1, Reservation res2)
    {
        return res1.Room.RoomNumber == res2.Room.RoomNumber &&
               res1.StartDate < res2.EndDate &&
               res2.StartDate < res1.EndDate;
    }
    
    public decimal GetReservationPrice() => Room.Price * _numberOfNights * (decimal)GetSeasonPricing();

    public float GetSeasonPricing()
    {
        float lowSeasonMultiplier = 0.8f; // 20% discount in low season
        float highSeasonMultiplier = 1.2f; // 20% increase in high season
        float totalMultiplier = 0.0f;

        DateTime currentDate = StartDate;
        while (currentDate < EndDate)
        {
            if (IsHighSeason(currentDate))
            {
                totalMultiplier += (highSeasonMultiplier);
            }
            else
            {
                totalMultiplier += (lowSeasonMultiplier);
            }
            currentDate = currentDate.AddDays(1);
        }

        return totalMultiplier / _numberOfNights;
    }

    private bool IsHighSeason(DateTime currentDate)
    {
        bool isWinterHighSeason = currentDate.Month is 12 or <= 1;
        bool isSummerHighSeason = currentDate.Month is 7 or 8;
        return isWinterHighSeason || isSummerHighSeason;
    }
}