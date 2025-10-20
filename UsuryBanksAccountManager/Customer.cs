namespace UsuryBanksAccountManager;

public class Customer
{
    private int Id { get; set; } // simulates primary key
    private string SocialSecurityNumber { get; set; }    // personal number used for login (personnummer)
    public Name Name { get; set; }
    private byte[] Password { get; set; }    // not used in reality, but only for example since it is much easier to implement.
    public Account Account { get; set; }    // simulates foreign key, one-to-one relation
    
    public Customer(string socialSecurityNumber, Name name, byte[] password)
    {
        SocialSecurityNumber = socialSecurityNumber;
        Name = name;
        Password = password;
    }
    
    public Customer(int id, string socialSecurityNumber, Name name, byte[] password, Account account)
    {
        Id = id;
        SocialSecurityNumber = socialSecurityNumber;
        Name = name;
        Password = password;
        Account = account;
    }
    
    public byte[] GetHashedPassword() => Password;

    public CustomerInfo GetCustomerInfo()
    {
        return new CustomerInfo
        {
            SocialSecurityNumber = SocialSecurityNumber,
            Name = Name.FirstName + " " + Name.LastName,
            Balance = Account.GetAccountInfo().Balance
        };
    }
}

public struct CustomerInfo
{
    public string SocialSecurityNumber { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
}

public struct Name(string firstName, string lastName)
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
}