namespace BlackJack;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorTop++;
        Console.WriteLine();
        
        Console.WriteLine("Welcome to BlackJack!");
        Console.WriteLine("What is your name? ");
        string playerName = Console.ReadLine() ?? "Player";
        
        Console.WriteLine($"Hello, {playerName}! Let's start the game.");
        Player player = new Player(playerName);
        
        Console.WriteLine("How much money do you want to insert? ");
        int money = int.Parse(Console.ReadLine() ?? "0");
        Console.WriteLine($"You have ${money} in Bank to start with.");
        player.AddMoneyToBank(money);
        
        Thread.Sleep(2000);
        Console.Clear();
        
        
        // Create a new instance of the game
        Game game = new Game(player);

        while (true)
        {
            game.StartGame();
        }
    }
}