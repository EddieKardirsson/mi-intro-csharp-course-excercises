namespace RipOffShopOnline;

public class Checkout(List<Product> productsInCart)
{
    public List<Product> ProductsInCart { get; } = productsInCart;
    
    private decimal _totalPrice = 0m;

    public void ShowCheckout()
    {
        Console.Clear();
        Console.WriteLine("Checkout");
        Console.WriteLine();
        
        ShoppingCart.CalculateCart(ProductsInCart);
        
        Console.WriteLine("Do you want to pay or go back? (y/n)");
        string? input = Console.ReadLine();
        if (input?.ToLower() == "y" && ProductsInCart.Count > 0)
            Payment();
        else if (input?.ToLower() == "n")
            return;
        else
            ShowCheckout();
        
    }
    
    public void Payment()
    {
        foreach (var product in ProductsInCart)
        {
            if (product.Quantity <= 0)
            {
                ProductsInCart.Remove(product);
                continue;
            }

            product.Quantity--;
            _totalPrice += product.PriceWithVat;
        }
        
        CalculateProfit();
        
        Console.WriteLine($"You paid: {_totalPrice} kr. Thank you for your purchase!");
    }

    public void CalculateProfit()
    {
        decimal profit = 0m;
        foreach (var product in ProductsInCart)
        {
            profit += product.Price - product.GetPurchasePrice();
        }

        StoreManager.AddProfit(profit);
    }
}