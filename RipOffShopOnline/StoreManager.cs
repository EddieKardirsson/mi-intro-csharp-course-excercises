namespace RipOffShopOnline;

public static class StoreManager
{
    public static List<Product> Products { get; private set; } = null!;

    public static ShoppingCart ShoppingCart { get; set; } = null!;

    private static int _productListCounter = 0;
    private static decimal _profit = 0m;
    
    public static decimal CalculateTotalPrice(List<Product> products) => products.Sum(p => p.Price);
    
    public static decimal CalculateTotalPriceWithVat(List<Product> products) => products.Sum(p => p.PriceWithVat);

    public static void StartSession()
    {
        // - Create a list of products
        Products = ProductsGenerator.GenerateProducts();
        
        // Initialize Shopping Cart
        ShoppingCart = new ShoppingCart();
        
        SessionLoop();
    }

    private static void DisplayMenuOptions()
    {
        Console.Clear();
        Console.WriteLine("Please select an option (1-4):");
        Console.WriteLine("\t1. Show products");
        Console.WriteLine("\t2. Show shopping cart");
        Console.WriteLine("\t3. Checkout");
        Console.WriteLine("\t4. Exit");
    }
    
    private static void SessionLoop() 
    {
        bool sessionActive = true;
        DisplayMenuOptions();
        
        while (sessionActive)
        {
            DisplayMenuOptions();
            string? input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                     ShowProducts();
                    break;
                case "2":
                    ShowShoppingCart();
                    break;
                case "3":
                    GoToCheckout();
                    break;
                case "4":
                    sessionActive = false;
                    Console.WriteLine("Thank you for visiting Rip Off Shop Online!");
                    break;
                case "0":
                    AdminLevel();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void ShowProducts()
    {
        Console.Clear();
        Console.WriteLine("Available Products");
        Console.WriteLine();
        
        for (int i = 0; i < Products.Count; i++)
        {
            var product = Products[i];
            Console.WriteLine($"{i + 1}. {product.Name} - {product.PriceWithVat} kr (In stock: {product.Quantity})");
        }
        
        Console.WriteLine();
        Console.WriteLine("Enter the number of the product to add to cart, or 'b' to go back:");
        string? input = Console.ReadLine();
        
        if (input?.ToLower() == "b")
        {
            Console.Clear();
            return;
        }
        
        if (int.TryParse(input, out int productNumber) && productNumber >= 1 && productNumber <= Products.Count)
        {
            var selectedProduct = Products[productNumber - 1];
            ShoppingCart.AddToCart(selectedProduct);
            Console.WriteLine($"{selectedProduct.Name} has been added to your cart.");
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    private static void ShowShoppingCart()
    {
        Console.Clear();
        Console.WriteLine("Shopping Cart");
        Console.WriteLine();
        
        ShoppingCart.CalculateCart(ShoppingCart.ProductsInCart);
        Console.ReadKey(false);
    }

    

    private static void GoToCheckout()
    {
        Checkout checkout = new Checkout(ShoppingCart.ProductsInCart);
        checkout.ShowCheckout();
        Console.ReadKey(false);
    }
    
    private static void AdminLevel()
    {
        Console.Clear();
        Console.WriteLine("Admin level");
        Console.WriteLine();
        
        Console.WriteLine("Enter the admin password: ");
        string password = Console.ReadLine();
        if (password == "qwerty")
        {
            Console.WriteLine("Welcome to admin level!");
            // TODO: Add admin level features here, such as tracking profits
        }
        else
        {
            Console.WriteLine("Invalid password. Please try again.");
        }
    }
    
    public static int IncrementProductListCounter() => ++_productListCounter;
    
    public static decimal GetProfit() => _profit;
    
    public static void AddProfit(decimal profit) => _profit += profit;
}