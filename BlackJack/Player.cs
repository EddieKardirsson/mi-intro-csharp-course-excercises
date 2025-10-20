namespace BlackJack;

public class Player
{
    public string Name { get; set; }
    public int Bank { get; private set; }
    private int Stake { get; set; }

    public bool HasLost { get; set; }
    
    public Player(string name) => Name = name;
    public void AddMoneyToBank(int amount) => Bank += amount;
    public void RemoveMoneyFromBank(int amount) => Bank -= amount;
    public void AddStake(int amount) => Stake += amount;
    public void RemoveStake(int amount) => Stake -= amount;
    public int GetStake() => Stake;
    public void ResetStake() => Stake = 0;
    public void AddStakeToBank()
    {
        Stake += Bank;
        Stake = 0;  // Reset stake
    }

    public Player()
    {
        HasLost = false;
    }
}
