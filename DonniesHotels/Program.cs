namespace DonniesHotels;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorTop++;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Welcome to Donnie's Hotels!");
        
        // TODO: Instantiate a hotel and display it's information
        //Hotel hotel = new Hotel("Stockholm");
        int floors = 4;
        int maxRooms = 6;
        HotelGenerator.GenerateHotel("Stockholm", floors, maxRooms, HotelGenerator.GenerateAverageMaxAreaPerFloor(maxRooms));
        HotelGenerator.PrintHotelInfo("Stockholm");
    }
}