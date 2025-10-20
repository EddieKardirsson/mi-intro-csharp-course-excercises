namespace GuessNumberGame;

class Program
{
    static void Main(string[] args)
    {
        Mikael mikael = new Mikael("Mikael");
        
        Console.WriteLine(mikael.Name);

        List<int> emptyList;

        Console.CursorTop++;
        Console.WriteLine();
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Guess the number between 1 and 10.");
            NumberComponent numberComponent = new NumberComponent(5);
            numberComponent.GenerateRandomNumber();
            while (numberComponent.Guesses <= numberComponent.MaxGuesses && !numberComponent.IsCorrect)
            {
                string? input = Console.ReadLine();
                bool bIsValidNumber = byte.TryParse(input, out byte number);

                if (!bIsValidNumber)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                
                numberComponent.GuessTheNumber(number);
                numberComponent.Guesses++;
                numberComponent.PrintResult();
            }
            Console.WriteLine("The game is over.");
            Console.WriteLine("Press any key to continue... or press Q to quit.");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
}

class Mikael
{
    public string Name { get; set; }

    public Mikael(string name)
    {
        Name = name;
    }
}

class NumberComponent
{
    private byte RandomNumber { get; set; }
    private byte Guess { get; set; }
    public byte Guesses { get; set; }
    public byte MaxGuesses { get; set; }
    public bool IsCorrect { get; private set; }

    public NumberComponent(byte maxGuesses)
    {
        MaxGuesses = maxGuesses;
    }
    
    public byte GenerateRandomNumber()
    {
        RandomNumber = (byte)new Random().Next(1, 10);
        return RandomNumber;
    }

    public void GuessTheNumber(byte guess)
    {
        Guess = guess;
        IsCorrect = Guess == RandomNumber;
    }
    
    public void PrintResult()
    {
        Console.WriteLine($"The guessed number was {Guess}.");
        if (IsCorrect)
        {
            Console.WriteLine($"You guessed the number {RandomNumber} correctly!");
        }
        else
        {
            Console.WriteLine("You guessed the wrong number!");
            Console.WriteLine(Guess < RandomNumber
                ? "The number is higher than your guess."
                : "The number is lower than your guess.");
        }
    }
}