namespace RipOffShopOnline;

public static class StoreManager
{
    public static List<Product> Products { get; set; }
    
    public static ShoppingCart ShoppingCart { get; set; }
    
    private static int _productListCounter = 0;
    
    public static decimal CalculateTotalPrice(List<Product> products) => products.Sum(p => p.Price);
    
    public static decimal CalculateTotalPriceWithVat(List<Product> products) => products.Sum(p => p.PriceWithVat);

    public static void StartSession()
    {
        // - Create a list of products
        Products = ProductsGenerator.GenerateProducts();
        
        // Initialize Shopping Cart
        ShoppingCart = new ShoppingCart();
        
        Session();
    }

    private static void Session()
    {
        DisplayMenuOptions();
    }

    private static void DisplayMenuOptions()
    {
        Console.WriteLine("\t1. Show products");
        Console.WriteLine("\t2. Show shopping cart");
        Console.WriteLine("\t3. Checkout");
        Console.WriteLine("\t4. Exit");
    }
    
    private static void SessionLoop() 
    {
        bool sessionActive = true;
        
        while (sessionActive)
        {
            Console.WriteLine("Please select an option (1-4):");
            string? input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    // TODO: ShowProducts();
                    break;
                case "2":
                    // TODO: ShowShoppingCart();
                    break;
                case "3":
                    // TODO: Checkout();
                    break;
                case "4":
                    sessionActive = false;
                    Console.WriteLine("Thank you for visiting Rip Off Shop Online!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    public static int IncrementProductListCounter() => ++_productListCounter;
}