namespace DonniesHotels;

public static class HotelGenerator
{
    public static List<Hotel> Hotels { get; } = new List<Hotel>();
    
    public static void GenerateHotel(string location, int floors, int maxRoomsPerFloor, float maxAreaPerFloor)
    {
        Hotel hotel = new Hotel(location, floors, maxRoomsPerFloor, maxAreaPerFloor);
        BuildHotel(hotel, floors, maxRoomsPerFloor);
    }
    
    private static void BuildHotel(Hotel hotel, int floors, int maxRoomsPerFloor)
    {
        int skippedFloors = 0;
        
        for (int i = 0; i < floors; ++i)
        {
            // reset max area per floor for each floor
            var maxAreaPerFloor = hotel.MaxAreaPerFloor;
            
            int floorNumber = (i + 1) * 100;
            for (int j = 0; j < maxRoomsPerFloor; ++j)
            {
                // Randomly select room type
                RoomType type = RandomRoomType(i);
                
                float roomArea = AreaPerRoomType(type);
                if (maxAreaPerFloor - roomArea < 0)
                {
                    // Not enough area left on this floor for another room
                    skippedFloors++;
                    continue;
                }
                
                // Assign room number. If a room is skipped, the room number is the same as the skipped room
                int roomNumber = floorNumber + j + 1 - skippedFloors;   
                int roomPrice = GenerateRoomPrice(type);
                
                Room newRoom = new Room(type, i + 1, roomPrice, roomNumber, roomArea);
                hotel.HotelRooms.Add(newRoom);
                maxAreaPerFloor -= roomArea;
            }
        }
        Hotels.Add(hotel);
    }

    private static RoomType RandomRoomType(int floorNumber)
    {
        // Base probability constants for better readability
        const float threeQuarters = 0.75f;
        const float half = 0.5f;
        const float quarter = 0.25f;
        const float dime = 0.1f;
        
        // default probabilities in lower floors
        float singleRoomProbability = threeQuarters;
        float doubleRoomProbability = half;
        float familyRoomProbability = quarter;
        float suiteRoomProbability = dime;
        
        // Set probability so that the probability of the higher tier rooms increases with higher floors
        singleRoomProbability -= floorNumber * 0.05f;
        doubleRoomProbability -= floorNumber * 0.03f;
        familyRoomProbability += floorNumber * 0.01f;
        suiteRoomProbability += floorNumber * 0.02f;
        
        // Ensure probabilities don't go below 0, or above max thresholds
        singleRoomProbability = Math.Max(0, singleRoomProbability);
        doubleRoomProbability = Math.Max(0, doubleRoomProbability);
        familyRoomProbability = Math.Min(familyRoomProbability, threeQuarters);
        suiteRoomProbability = Math.Min(suiteRoomProbability, half);
        
        // Calculate total probability for normalization
        float totalProbability = 
            singleRoomProbability + doubleRoomProbability + familyRoomProbability + suiteRoomProbability;
        
        // Generate random value between 0 and total probability
        Random random = new Random();
        float randomValue = (float)random.NextDouble() * totalProbability;
        
        // Select room type based on random value
        float cumulativeProbability = 0;
        
        cumulativeProbability += singleRoomProbability;
        if (randomValue < cumulativeProbability)
            return RoomType.Single;
        
        cumulativeProbability += doubleRoomProbability;
        if (randomValue < cumulativeProbability)
            return RoomType.Double;
        
        cumulativeProbability += familyRoomProbability;
        if (randomValue < cumulativeProbability)
            return RoomType.Family;
        
        return RoomType.Suite;
    }

    // return area per room type in square meters
    private static float AreaPerRoomType(RoomType type)
    {
        switch (type)
        {
            case RoomType.Single:
                return 15; 
            case RoomType.Double:
                return 30;
            case RoomType.Family:
                return 45;
            case RoomType.Suite:
                return 60;
            default:
                return 0;
        }
    }

    // return price per room in kr. This is the base price, which can be modified later based on high or low season.
    private static int GenerateRoomPrice(RoomType type)
    {
        switch (type)
        {
            case RoomType.Single:
                return 1200;
            case RoomType.Double:
                return 1800;
            case RoomType.Family:
                return 2600;
            case RoomType.Suite:
                return 3900;
            default:
                return 0;
        }
    }

    public static float GenerateAverageMaxAreaPerFloor(int maxRoomsPerFloor)
    {
        Array roomTypes = Enum.GetValues(typeof(RoomType));
        float average = AreaPerRoomType(RoomType.Single) + 
                        AreaPerRoomType(RoomType.Double) + 
                        AreaPerRoomType(RoomType.Family) + 
                        AreaPerRoomType(RoomType.Suite) / roomTypes.Length; // divided by 4 because there are 4 room types
        float maxAreaPerFloor = average * maxRoomsPerFloor;
        Console.WriteLine($"Suggested Max Area per Floor: {maxAreaPerFloor} m2");
        return maxAreaPerFloor;
    }

    public static void PrintHotelInfo(string hotelName)
    {
        Hotel? hotel = Hotels.Find(hotel => hotel.Location == hotelName);
        if (hotel == null)
        {
            Console.WriteLine("Hotel not found.");
            return;
        }
        
        hotel.HotelRooms.ForEach(room =>
        {
            Console.WriteLine(
                $"Room {room.RoomNumber} | Floor: {room.Floor} | Type: {room.Type} | " +
                $"Price: {room.Price} kr/night | Area: {room.Area} m2 | Booked: {room.IsBooked}");
        });
        
        
    }
}