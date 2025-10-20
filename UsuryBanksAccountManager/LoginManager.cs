using System.Security.Cryptography;
using System.Text;

namespace UsuryBanksAccountManager;

// static because this doesn't need multiple instances of the class (singleton)
public static class LoginManager
{
    private static Customer? ActiveCustomer { get; set; }  // Handles the user session
    private static bool IsLoggedIn => ActiveCustomer != null;
    private static List<Customer>? CustomersList { get; set; }
    private static List<Account>? AccountsList { get; set; }

    private static int _customerIdCounter = 0; // Simulates auto-increment primary key in a database
    private static int _accountIdCounter = 0;  
    private static int _transactionIdCounter = 0; 
    
    
    // Use a key for AES-256 (32 bytes)
    private static readonly byte[] EncryptionKey = GenerateKey();
    
    public static void Login(string socialSecurityNumber, string password)
    {
        if (CustomersList == null || CustomersList.Count == 0)
        {
            Console.WriteLine("\nNo registered customers found. Please register first.");
            return;
        }

        //string encryptedSsn = Convert.ToBase64String(EncryptSensitiveData(socialSecurityNumber));
        byte[] hashedPassword = HashPassword(password);

        foreach (var customer in CustomersList)
        {
            string decryptedSsn = DecryptSensitiveData(customer.GetCustomerInfo().SocialSecurityNumber);
            Console.WriteLine($"Decrypted SSN: {decryptedSsn}");
            Console.WriteLine($"login SSN matches with stored SSN: {decryptedSsn.Equals(socialSecurityNumber)}");
            Console.WriteLine($"login password matches with stored password: {hashedPassword.SequenceEqual(customer.GetHashedPassword())}");
            if (decryptedSsn.Equals(socialSecurityNumber) &&
                customer.GetHashedPassword().SequenceEqual(hashedPassword))
            {
                ActiveCustomer = customer;
                Console.WriteLine($"\nLogin successful. Welcome back, {DecryptSensitiveData(customer.Name.FirstName)}!");
            }
        }

        if (ActiveCustomer == null)
        {
            Console.WriteLine("Login failed. Invalid social security number or password. Register an account or try again.");
        }

        //Console.WriteLine("\nLogin failed. Invalid social security number or password.");
        
        CreateSession();
    }

    private static void CreateSession()
    {
        Console.Clear();
        if (IsLoggedIn)
            Console.WriteLine($"\nSession created for {DecryptSensitiveData(ActiveCustomer!.Name.FirstName)}.");
        else
            Console.WriteLine("\nNo active session. Please login.");
        
        Timer sessionTimer = StartSessionTimer();
        
        // use sessionTimer in a while loop to keep the session alive
        while (IsLoggedIn)
        {
            SessionMenu();
        }
    }

    private static void SessionMenu()
    {
        Account activeAccount = ActiveCustomer!.Account;
        string activeCustomerName = 
            DecryptSensitiveData(ActiveCustomer.Name.FirstName) + 
            DecryptSensitiveData(ActiveCustomer.Name.LastName);
        
        Console.WriteLine(
            $"1. Check Balance " +
            $"2. Deposit " +
            $"3. Withdraw " +
            $"Q. Logout"
        );

        ConsoleKeyInfo menuInput = Console.ReadKey(false);
        switch (menuInput)
        {
            case var keyInfo when keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1:
                Console.WriteLine($"\nYour Balance: {activeAccount.GetBalance()}");
                break;
            case var keyInfo when keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2:
                Console.WriteLine("\nEnter amount to deposit:");
                string? depositInput = Console.ReadLine();
                decimal depositAmount;
                while (!decimal.TryParse(depositInput, out depositAmount) || depositAmount <= 0)
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid amount to deposit.");
                    depositInput = Console.ReadLine();
                }
                activeAccount.CreateTransaction(TransactionType.Deposit, depositAmount);
                Console.WriteLine($"\nYour Balance: {activeAccount.GetBalance()}");
                break;
            case var keyInfo when keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3:
                Console.WriteLine("\nEnter amount to withdraw:");
                string? withdrawInput = Console.ReadLine();
                decimal withdrawAmount;
                while (!decimal.TryParse(withdrawInput, out withdrawAmount) || withdrawAmount <= 0)
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid amount to withdraw.");
                    withdrawInput = Console.ReadLine();
                }
                activeAccount.CreateTransaction(TransactionType.Withdrawal, withdrawAmount);
                Console.WriteLine($"\nYour Balance: {activeAccount.GetBalance()}");
                break;
            case var keyInfo when keyInfo.Key == ConsoleKey.Q || keyInfo.Key == ConsoleKey.Escape:
                Logout();
                break;
            default:
                Console.WriteLine("\nInvalid choice. Please try again.");
                break;
        }
    }

    private static Timer StartSessionTimer()
    {
        TimeSpan sessionDuration = TimeSpan.FromMinutes(15);
        Console.WriteLine($"Session will expire in {sessionDuration.TotalMinutes} minutes.");
        return new Timer(state => Logout(), null, sessionDuration, sessionDuration);
    }

    public static void Logout()
    {
        Console.WriteLine("\nLogging out...");
        ActiveCustomer = null;
        Thread.Sleep(1000);
        Console.WriteLine("Logged out.");
    }

    public static void Register(string socialSecurityNumber, Name name, string password)
    {
        string encryptedSocialSecurityNumber = Convert.ToBase64String(EncryptSensitiveData(socialSecurityNumber));
        string encryptedFirstName = Convert.ToBase64String(EncryptSensitiveData(name.FirstName));
        string encryptedLastName = Convert.ToBase64String(EncryptSensitiveData(name.LastName));
        byte[] hashedPassword = HashPassword(password);
        
        CreateCustomer(encryptedSocialSecurityNumber, encryptedFirstName, encryptedLastName, hashedPassword);
        if (ActiveCustomer != null)
        {
            Account account = ActiveCustomer.Account;
            account.Customer = ActiveCustomer;
            CustomersList ??= new List<Customer>();
            AccountsList ??= new List<Account>();
            CustomersList.Add(ActiveCustomer);
            AccountsList.Add(account);
            Console.WriteLine("\nRegistration successful. You are now logged in.");
            CreateSession();
        }
    }

    private static void CreateCustomer(string encryptedSsn, string encryptedFirstName, string encryptedLastName, byte[] hashedPassword)
    {
        ActiveCustomer = new Customer(
            ++_customerIdCounter, 
            encryptedSsn,  
            new Name(encryptedFirstName, encryptedLastName),
            hashedPassword, 
            new Account(++_accountIdCounter, 0m)
        );
    }

    public static void ChangePassword()
    {
        // TODO: Implement password change functionality
    }
    
    private static byte[] GenerateKey()
    {
        // Create a 32-byte key for AES-256 (bad practice to use a key in plain text, but for this example it's fine)
        var keyString = "MySecretKey12345MySecretKey67890"; // 32 characters = 32 bytes
        return Encoding.UTF8.GetBytes(keyString);
    }

    public static byte[] EncryptSensitiveData(string sensitiveData)
    {
        // Encrypt the sensitive data using AES-256 with a key and IV
        using Aes aes = Aes.Create();
        aes.Key = EncryptionKey;
        aes.GenerateIV(); // Generate a random IV for each encryption

        // Encrypt the data with memory stream and crypto stream. The encrypted data is stored in memoryStream.
        using var encryptor = aes.CreateEncryptor();
        using var memoryStream = new MemoryStream();
        // Prepend IV to the encrypted data
        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        // Crypto stream encrypts the data and writes it to memoryStream
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            // Write the data to the stream
            streamWriter.Write(sensitiveData);
        }
                
        // Get the encrypted data from memoryStream and convert it to a byte array and return it as a byte array
        var encryptedData = memoryStream.ToArray();
        string encryptedDataString = Convert.ToBase64String(encryptedData);
        Console.WriteLine($"Encrypted: {encryptedDataString}"); // just to test
        return encryptedData;
    }

    public static string DecryptSensitiveData(string encryptedDataInput)
    {
        byte[] encryptedData = Convert.FromBase64String(encryptedDataInput);
        using Aes aes = Aes.Create();
        aes.Key = EncryptionKey;
            
        // Extract IV from the beginning of the encrypted data
        byte[] iv = new byte[aes.BlockSize / 8];
        Array.Copy(encryptedData, 0, iv, 0, iv.Length);
        aes.IV = iv;
            
        // Get the actual encrypted data (without IV)
        byte[] cipherText = new byte[encryptedData.Length - iv.Length];
        Array.Copy(encryptedData, iv.Length, cipherText, 0, cipherText.Length);

        using var decryptor = aes.CreateDecryptor();
        using var cipherStream = new MemoryStream(cipherText);
        using var decryptionStream = new CryptoStream(cipherStream, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(decryptionStream);
        
        string output = reader.ReadToEnd();
        Console.WriteLine($"Decrypted: {output}");
        return output;
    }

    public static byte[] HashPassword(string password) => SHA256.HashData(Encoding.UTF8.GetBytes(password));
    
    public static string HashPasswordToString(byte[] hashedPassword)
    {
        return Convert.ToBase64String(hashedPassword);
    }
    
    // Helper functions:
    public static int GetNextTransactionId() => ++_transactionIdCounter;
    
    // Debug functions:
    public static void PrintCustomers() => CustomersList?.ForEach(customer => Console.WriteLine(customer.GetCustomerInfo()));
    public static void PrintAccounts() => AccountsList?.ForEach(account => Console.WriteLine(account.GetAccountInfo()));
}