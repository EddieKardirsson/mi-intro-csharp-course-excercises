namespace UsuryBanksAccountManager;

class Program
{
    static void Main(string[] args)
    {
        Console.CursorTop++;
        DisplayWelcomeMessage();

        Thread.Sleep(2000);
        Console.WriteLine();

        //TestEncryptionAndHashing();

        while (true)
        {
            DisplayMainMenu(args);
        }
    }
    
    private static void DisplayWelcomeMessage()
    {
        Console.WriteLine("***************************");
        Console.WriteLine("* Welcome to Usury Banks! *");
        Console.WriteLine("***************************");
    }

    private static void DisplayMainMenu(string[] args)
    {
        Console.WriteLine("Please login or register to continue.");
        Console.WriteLine();
        Console.WriteLine("Press 1 to login, 2 to register or 3 to exit.");
        ConsoleKeyInfo mainMenuChoice = Console.ReadKey(false);
        switch (mainMenuChoice)
        {            
            case var keyInfo when keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1:
                Console.WriteLine();
                Console.WriteLine("Please enter your social security number and password to login.");
                Console.WriteLine();
                Console.WriteLine("Social security number: ");
                string ssn = ValidateSocialSecurityInput().ToString();
                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine() ?? string.Empty;
                LoginManager.Login(ssn, password);
                break;
            case var keyInfo when keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2:
                Console.WriteLine();
                string newSsn = RegisterSocialSecurityNumber(); 
                Name customerName = RegisterCustomerName();
                string registeredPassword = RegisterPassword();
                LoginManager.Register(newSsn, customerName, registeredPassword);
                break;
            case var keyInfo when keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3:
                Console.WriteLine();
                Console.WriteLine("Thank you for using Usury Banks Account Manager. Goodbye!");
                Thread.Sleep(2000);
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine();
                Console.WriteLine("Invalid choice. Please try again.");
                Thread.Sleep(2000);
                Main(args); // Restart the main menu
                break;
        }
    }

    private static string RegisterSocialSecurityNumber()
    {
        Console.WriteLine("What is your social security number? (Personnummer, format: YYYYMMDDXXXX)");
        double input = ValidateSocialSecurityInput();
        return input.ToString();
    }

    private static double ValidateSocialSecurityInput()
    {
        string? inputSsn = Console.ReadLine();
        double input;
        while(!Double.TryParse(inputSsn, out input) || input < 100000000000 || input > 999999999999)
        {
            Console.WriteLine("Invalid input. Please enter a valid social security number (format: YYYYMMDDXXXX):");
            inputSsn = Console.ReadLine();
        }

        return input;
    }

    private static Name RegisterCustomerName()
    {
        Console.WriteLine("What is your first name?");
        string firstName = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("What is your last name?");
        string lastName = Console.ReadLine() ?? string.Empty;
        return new Name(firstName, lastName);
    }

    private static string RegisterPassword()
    {
        Console.WriteLine("What is your password?");
        string password = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Confirm your password: ");
        string confirmPassword = Console.ReadLine() ?? string.Empty;
        while (password != confirmPassword)
        {
            Console.WriteLine("Passwords do not match. Please enter your password again:");
            password = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Confirm your password: ");
            confirmPassword = Console.ReadLine() ?? string.Empty;
        }
        return confirmPassword;
    }
    
    private static void TestEncryptionAndHashing()
    {
        // Test encryption and hashing
        const string sensitiveData = "Mikael Mikael";
        Console.WriteLine($"Original sensitive data: {sensitiveData}");
        var encryptedData = LoginManager.EncryptSensitiveData(sensitiveData);
        var decryptedData = LoginManager.DecryptSensitiveData(Convert.ToBase64String(encryptedData));
        
        const string password = "VeryBadPassword";
        Console.WriteLine($"\nOriginal password: {password}");
        byte[] hashedPassword = LoginManager.HashPassword(password);
        Console.WriteLine($"Hashed password: {LoginManager.HashPasswordToString(hashedPassword)}");
    }
}