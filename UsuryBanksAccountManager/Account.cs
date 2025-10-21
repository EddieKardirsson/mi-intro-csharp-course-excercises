namespace UsuryBanksAccountManager;

public class Account
{
    private int Id { get; set; } // simulates primary key
    private decimal Balance { get; set; }
    private List<Transaction> Transactions { get; set; } // simulates one-to-many relation
    public Customer Customer { get; set; }  // simulates foreign key, one-to-one relation

    public Account(int balance)
    {
        Balance = balance;
        Transactions = new List<Transaction>();
    }

    public Account(int id, decimal balance)
    {
        Id = id;
        Balance = balance;
        Transactions = new List<Transaction>();
    }

    public void CreateTransaction(TransactionType type, decimal amount)
    {
        if (type == TransactionType.Withdrawal && amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
        }
        else if (type == TransactionType.Deposit && amount < 0)
        {
            Console.WriteLine("Invalid deposit amount.");
        }
        else
        {
            Transactions.Add(new Transaction(LoginManager.GetNextTransactionId(), type, amount, DateTime.Now, Customer));
            Balance += type == TransactionType.Deposit ? amount : -amount;
        }
    }

    public void PrintTransactions()
    {
        if (Transactions.Count == 0)
        {
            Console.WriteLine("No transactions found.");
            return;
        }
        foreach (var transaction in Transactions)
        {
            Console.WriteLine(
                $"{transaction.GetTransactionInfo().CustomerName} - " +
                $"{transaction.GetTransactionInfo().Type} - " +
                $"{transaction.GetTransactionInfo().Amount} - " +
                $"{transaction.GetTransactionInfo().Date}");
        }
    }
    
    public void PrintBalance() => Console.WriteLine($"Balance: {Balance}");
    public decimal GetBalance() => Balance;

    public AccountInfo GetAccountInfo()
    {
        return new AccountInfo
        {
            CustomerName = Customer.Name.FirstName + " " + Customer.Name.LastName,
            Balance = Balance
        };
    }
}

public struct AccountInfo
{
    public string CustomerName { get; set; }
    public decimal Balance { get; set; }
}