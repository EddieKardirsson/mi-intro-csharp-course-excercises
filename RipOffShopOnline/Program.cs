namespace RipOffShopOnline;

/*
 * TASK:
 * Create a console application where the user can buy a product. By putting products into a shopping cart.
 * After at least one product is added to the cart, the user can checkout and see the total price and confirm order.
 *
 * BONUS:
 * - Add a VAT on products and let the user see the total price with VAT.
 * - Add store purchase prices on products, so that the admin can see the orders and see how much profit has been generated
 *
 * EXTRA:
 * - Connect this project with the UsuryBanksAccountManager project. To have a simulated payment method.
 * */

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // PSEUDO CODE
        // - Welcome Text
        DisplayWelcomeMessage();
        // - Create a Store Manager class - done
        StoreManager.StartSession();
        // - Add products to the shopping cart
        // - Checkout
        // - Print total price
        // - Print total price with VAT
        // - Print total price with store purchase price
        // - Print total profit
    }

    static void DisplayWelcomeMessage()
    {
        var text = "Welcome to the Rip Off Shop Online!";
        int padding = 2;
        int contentWidth = text.Length;
        int boxWidth = contentWidth + padding * 2;
        
        // Top border
        Console.WriteLine("╔" + new string('═', boxWidth) + "╗");
        
        Console.WriteLine("║" + new string(' ', boxWidth) + "║");
        
        // Content with side borders
        Console.WriteLine("║" + new string(' ', padding) + text + new string(' ', padding) + "║");
        
        Console.WriteLine("║" + new string(' ', boxWidth) + "║");
        
        // Bottom border
        Console.WriteLine("╚" + new string('═', boxWidth) + "╝");
        Console.WriteLine();
    }
}