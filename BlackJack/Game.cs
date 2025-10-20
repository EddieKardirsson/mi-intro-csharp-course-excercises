namespace BlackJack;

public class Game
{
    private const int MINIMUM_STAKE = 10;
    
    // Properties
    // TODO: Instance of Deck
    public Deck Deck { get; set; }
    public List<Card> PlayerCards { get; set; }
    public List<Card> DealerCards { get; set; }
    public byte DealerScore { get; set; }
    public byte PlayerScore { get; set; }
    
    public Player Player { get; set; }

    public Game(Player player)
    {
        Player = player;
        PlayerCards = new List<Card>();
        DealerCards = new List<Card>();
    }

    public void StartGame()
    {
        Deck = new Deck();
        Deck.Shuffle();
        PlayerCards.Clear();    // Make sure it's empty before the round starts
        DealerCards.Clear();
        Console.WriteLine("Game started! Enter your stake amount: ");
        int stake = int.Parse(Console.ReadLine() ?? "0");
        while (stake <= MINIMUM_STAKE)
        {
            Console.WriteLine($"You must enter a stake amount greater than ${MINIMUM_STAKE}.");
            stake = int.Parse(Console.ReadLine() ?? "0");
        }
        Player.RemoveMoneyFromBank(stake);
        Console.WriteLine($"You entered ${stake}.");
        Console.WriteLine($"You have ${Player.Bank} left." );
        PlayGame();
    }

    public void PlayGame()
    {
        Console.WriteLine("\nGame is running, shuffled and ready to play.");
        // wait 2 second
        Thread.Sleep(2000);
        Console.Clear();
        DealInitialCards();
        InitializeGameRound();

        PlayerRound();
        if (Player.HasLost == false)
            DealerRound();
    }

    private void DealInitialCards()
    {
        // Deal 2 cards to each player
        PlayerCards.Add(Deck.DrawCard());
        Thread.Sleep(500);
        DealerCards.Add(Deck.DrawCard());
        Thread.Sleep(500);
        PlayerCards.Add(Deck.DrawCard());
        Thread.Sleep(500);
        DealerCards.Add(Deck.DrawCard());
        Thread.Sleep(500);
        // Sort the cards in ascending order so the Ace is always the last card in the hand
        PlayerCards.Sort();
        DealerCards.Sort();
    }

    private void InitializeGameRound()
    {
        CalculateScore();
        PrintScore();
        PrintHand();
    }

    private void CalculateScore()
    {
        PlayerScore = 0;
        DealerScore = 0;
        CalculatePlayerScore();
        CalculateDealerScore();
        return;
        
        void CalculatePlayerScore()
        {
            foreach (var card in PlayerCards)
            {
                // Ace can be worth 1 or 11 check if player has an ace
                if (PlayerCards.Any(c => c.Rank == "Ace"))
                {
                    // If player has an ace, check if the score is 11 or more
                    if (card.Rank == "Ace" && PlayerScore > 10)
                    {
                        // If score is 11 or more, change the value of the ace to 1
                        card.ChangeValue(1);
                    }
                }
                PlayerScore += card.Value;
            }
        }

        void CalculateDealerScore()
        {
            foreach (var card in DealerCards)
            {
                if (DealerCards.Any(c => c.Rank == "Ace"))
                {
                    if (card.Rank == "Ace" && DealerScore > 10)
                    {
                        card.ChangeValue(1);
                    }
                }
                DealerScore += card.Value;
            }
        }
        
    }

    private void PrintScore()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine($"Player Score: {PlayerScore}");
        Console.WriteLine($"Dealer Score: {DealerScore}");
    }

    private void PrintHand()
    {
        Console.WriteLine();
        Console.Write("Player Hand: ");
        foreach (var card in PlayerCards)
        {
            Console.Write($"{card.Rank} of {card.Suit}, ");
        }

        Console.WriteLine();
        Console.Write("Dealer Hand: ");
        foreach (var card in DealerCards)
        {
            Console.Write($"{card.Rank} of {card.Suit}, ");
        }
    }

    public void PlayerRound()
    {
        Console.WriteLine("\nPress any key to continue.");
        Console.ReadKey(true);  // wait for user input to continue
        DisplayPlayerOptions();
        var input = Console.ReadKey();
        switch (input.Key)
        {
            case ConsoleKey.D1:
                PlayerCards.Add(Deck.DrawCard());
                InitializeGameRound();
                if (PlayerScore > 21)
                {
                    Console.WriteLine("\nYou busted! Dealer wins.");
                    Player.HasLost = true;
                    Player.ResetStake();
                    break;
                }
                if (PlayerScore == 21)
                {
                    Console.WriteLine("\nYou got BlackJack! You win!");
                    Player.AddStake(Player.GetStake() * 3);
                    Player.AddStakeToBank();
                    break;
                }

                if (PlayerScore > DealerScore)
                {
                    Console.WriteLine("\nPlayer is ahead, do you want to hit or stand?");
                }
                PlayerRound();
                break;
            case ConsoleKey.D2:
                Console.WriteLine("\nPlayer stands.");
                if(PlayerScore > DealerScore)
                {
                    Console.WriteLine("\nPlayer wins!");
                    Player.AddStake(Player.GetStake() * 2);
                    Player.AddStakeToBank();
                }
                break;
        }
    }
    
    public void DealerRound()
    {
        while (DealerScore < 21)
        {
            Console.Clear();
            DealerCards.Add(Deck.DrawCard());
            
            // If dealer score is less than 17, continue to draw cards otherwise end the round
            if (DealerScore < 17)
            {
                InitializeGameRound();
                Console.WriteLine("Dealer is drawing...");
            }
            else
            {
                Console.WriteLine("Dealer stands");
            }

            Thread.Sleep(2000);

            if (DealerScore > 21)
            {
                Console.WriteLine("\nDealer busted! Player wins.");
                Player.AddStake(Player.GetStake() * 2);
                Player.AddStakeToBank();
                return;
            }
            if (PlayerScore < DealerScore)
            {
                Console.WriteLine("\nDealer wins!");
                return;
            }
            if (PlayerScore > DealerScore && PlayerScore <= 21)
            {
                Console.WriteLine("\nPlayer wins!");
                return;
            }
        }
    }

    private static void DisplayPlayerOptions()
    {
        Console.Clear();
        Console.WriteLine("Player round!\n");
        Console.WriteLine("What do you want to do?");
        Console.WriteLine("1. Hit");
        Console.WriteLine("2. Stand");
        Console.WriteLine("3. Quit");
    }

    
}