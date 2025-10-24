namespace RipOffShopOnline;

public class Checkout(List<Product> productsInCart)
{
    public List<Product> ProductsInCart { get; } = productsInCart;

    public void ShowCheckout()
    {
        Console.Clear();
        Console.WriteLine("Checkout");
        Console.WriteLine();
        Console.WriteLine("Your cart:");
        Console.WriteLine("------------------------------------------------------");
        
        foreach (var product in ProductsInCart)
        {
            Console.WriteLine($"{product.Name} - {product.PriceWithVat} kr");
        }
        
        decimal price = ProductsInCart.Sum(p => p.Price);
        decimal totalPrice = ProductsInCart.Sum(p => p.PriceWithVat);
        
        Console.WriteLine("------------------------------------------------------");
        Console.WriteLine($"Total: {totalPrice} kr");
        Console.WriteLine($"VAT amount: {totalPrice - price} kr");
    }
}