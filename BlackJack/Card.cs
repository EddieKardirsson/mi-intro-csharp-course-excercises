namespace BlackJack;

public struct Card : IComparable<Card>
{
    public string Suit { get; set; }
    public string Rank { get; set; }
    public byte Value { get; set; }
    
    public Card(string suit, string rank, byte value)
    {
        Suit = suit;
        Rank = rank;
        Value = value;
    }
    
    public void ChangeValue(byte value)
    {
        Value = value;
    }

    public int CompareTo(Card other)
    {
        return Value.CompareTo(other.Value); // Ascending order by value
    }
}