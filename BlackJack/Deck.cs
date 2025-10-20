namespace BlackJack;

public class Deck
{
    private List<Card>? Cards { get; set; }

    public Deck()
    {
        GenerateDeck();
    }

    private void GenerateDeck()
    {
        Cards = new List<Card>();
        string[] suits = ["\u2660", "\u2663", "\u2665", "\u2666"];
        string[] ranks = [
            "Two", "Three", "Four", "Five", "Six", "Seven", 
            "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace"
        ];
        byte[] values = [2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11];
        
        for (int i = 0; i < suits.Length; i++)
        {
            for (int j = 0; j < ranks.Length; j++)
            {
                Cards.Add(new Card(suits[i], ranks[j], values[j]));
            }
        }
    }

    public void Shuffle()
    {
        Random rand = new Random();
        if (Cards != null)
        {
            int n = Cards.Count;
        
            for (int i = n - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                // Swap Cards[i] with the element at a random index
                (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
            }
        }
    }

    public Card DrawCard()
    {
        // pop a card from the Deck
        Card card = Cards.ElementAt(0);
        Cards.RemoveAt(0);
        return card;
    }
}

