namespace RipOffShopOnline;

public class Checkout(List<Product> productsInCart)
{
    public List<Product> ProductsInCart { get; } = productsInCart;

    public void ShowCheckout()
    {
        Console.Clear();
        Console.WriteLine("Checkout");
        Console.WriteLine();
        
        ShoppingCart.CalculateCart(ProductsInCart);
        
        // TODO: Payment()
    }
}