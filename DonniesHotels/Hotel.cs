namespace DonniesHotels;

public class Hotel
{
    public List<Room> HotelRooms { get; } = new List<Room>();
    public Dictionary<Room, Guest?> Bookings { get; } = new Dictionary<Room, Guest?>();
    
    public Dictionary<Room, Reservation> Reservations { get; } = new Dictionary<Room, Reservation>();
    public static List<Guest> Guests { get; } = new List<Guest>();  // for all hotels
    public static Guest? LoggedInGuest { get; set; } = null;
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

    public void HotelMenu()
    {
        GenerateTestGuest();
        Console.Clear();
        Console.WriteLine($"Welcome to Donnie's Hotels {Location}!");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("Hotel Menu");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("| 1. Login/Register                                       |");
        Console.WriteLine("| 2. Book Room                                            |");
        Console.WriteLine("| 3. Check Availability                                   |");
        Console.WriteLine("| 4. Manage Reservation                                   |");
        Console.WriteLine("| 5. Logout                                               |");
        Console.WriteLine("| 6. Calculate Revenue                                    |");
        Console.WriteLine("| Q. Exit                                                 |");
        Console.WriteLine("-----------------------------------------------------------");
        Console.Write("Select an option: ");
        TODO: MenuSelection();
    }

    private void MenuSelection()
    {
        char input = Console.ReadKey().KeyChar;
        switch (input)
        {
            case '1': 
                LoginOrRegister(); 
                break;
            case '2':
                BookRoom();
                Console.ReadKey(false);
                break;
            case '3':
                CheckAvailability();
                Console.ReadKey(false);
                break;
            case '4':
                // TODO: ManageReservation
                break;
            case '5':
                // TODO: Logout
                break;
            case '6':
                // TODO: CalculateRevenue
                break;
            case 'q':
            case 'Q':
                Exit();
                break;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }

    private static void LoginOrRegister()
    {
        Console.Clear();
        Console.WriteLine("Login or Register");
        Console.WriteLine();
        Console.WriteLine("Enter your email: ");
        string email = Console.ReadLine();
        Guest? guest = Guests.Find(g => g.Email == email);
        if (guest != null)
        {
            Console.WriteLine("Welcome back!");
            Login(guest);
        }
        else
        {
            Console.WriteLine("Guest not found.");
            Console.WriteLine("Please register an account.");
            Console.Write("\nEnter your name: ");
            string? name = Console.ReadLine();
            Console.Write("Enter your age: ");
            string? ageInput = Console.ReadLine();
            int age;
            while (!int.TryParse(ageInput, out age))
            {
                Console.WriteLine("Invalid age. Please enter a valid age.");
                ageInput = Console.ReadLine();
            }
            guest = new Guest(name, age, email);
            RegisterGuest(guest);
        }
        Console.ReadKey(false);
    }

    // TODO: Register Guest
    public static void RegisterGuest(Guest guest)
    {
        Guests.Add(guest);
        LoggedInGuest = guest;
        Console.WriteLine($"Guest {guest.Name} Registered!");
    }
    
    // TODO: Login
    public static void Login(Guest guest) => LoggedInGuest = guest;
    
    // TODO: Book Room
    public void BookRoom()
    {
        Console.Clear();
        if (CheckUserLoginStatus()) return;
        
        Console.Clear();
        Console.WriteLine("Book Room");
        Console.WriteLine();
        
        var roomNumber = RequestRoomNumberInput();
        Room? room = CheckAvailability(roomNumber);

        if (room != null)
        {
            if (IsRoomOccupied(room)) return;
            Bookings[room] = LoggedInGuest;
            LoggedInGuest.BookingId = Bookings[room]?.BookingId;
            LoggedInGuest.BookedRoom = room;
            
            // TODO: Console.WriteLine("Enter check-in date: ");
            Console.WriteLine($"Room {room.RoomNumber} booked successfully.");
        }
    }

    // TODO: Check Availability
    public void CheckAvailability()
    {
        Console.Clear();
        Console.WriteLine("Check Availability");
        var roomNumber = RequestRoomNumberInput();
        Room? room = CheckAvailability(roomNumber);
    }

    public Room? CheckAvailability(int roomNumber)
    {
        Room? room = HotelRooms.Find(r => r.RoomNumber == roomNumber);
        if (room != null)
        {
            if (IsRoomOccupied(room)) return null;
            Console.Clear();
            Console.WriteLine($"Room {room.RoomNumber} is available.");
            return room;
        }
        Console.WriteLine("Room not found.");
        return null;
    }
    
    // TODO: Manage Reservation
    public void ManageReservation()
    {
        if (CheckUserLoginStatus()) return;
        
        // TODO: Logic for managing reservation
    }
    
    // TODO: Logout
    
    // TODO: Calculate Revenue
    
    // TODO: Logout
    public static void Exit()
    {
        Console.WriteLine("\nGoodbye");
        Environment.Exit(0);
    }
    
    // ----------------------------------------- Helper methods------------------------------------------------------
    private static int RequestRoomNumberInput()
    {
        Console.Write("\nEnter room number: ");
        string? roomNumberInput = Console.ReadLine();
        int roomNumber;
        while (!int.TryParse(roomNumberInput, out roomNumber))
        {
            Console.WriteLine("Invalid room number. Please enter a valid room number.");
            roomNumberInput = Console.ReadLine();
        }

        return roomNumber;
    }
    
    private bool IsRoomOccupied(Room room)
    {
        if (Bookings.ContainsKey(room) && Bookings[room] != null)
        {
            Console.WriteLine($"Room {room.RoomNumber} is already booked.");
            return true;
        }

        return false;
    }
    
    private static bool CheckUserLoginStatus()
    {
        if (LoggedInGuest == null)
        {
            Console.WriteLine("You are not logged in. Please login to continue.");
            return true;
        }

        return false;
    }
    
    // Generate test guest for debug purpose
    public static void GenerateTestGuest()
    {
        Guest guest = new Guest("Mikael Mikael", 31, "mikaelmikael@fakemail.com");
        Guests.Add(guest);
    }
}