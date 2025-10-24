namespace RipOffShopOnline;

public class ShoppingCart
{
    public List<Product> ProductsInCart { get; }

    public ShoppingCart()
    {
        ProductsInCart = new List<Product>();
    }
    
    public void AddToCart(Product product) => ProductsInCart.Add(product);

    public void RemoveFromCart(Product product) => ProductsInCart.Remove(product);

    public decimal CalculateTotalPrice() => StoreManager.CalculateTotalPrice(ProductsInCart);
    public decimal CalculateTotalPriceWithVat() => StoreManager.CalculateTotalPriceWithVat(ProductsInCart);

    public static void CalculateCart(List<Product> products)
    {
        Console.WriteLine("Your cart:");
        Console.WriteLine("------------------------------------------------------");
        
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name} - {product.PriceWithVat} kr");
        }
        
        decimal price = products.Sum(p => p.Price);
        decimal totalPrice = products.Sum(p => p.PriceWithVat);
        
        Console.WriteLine("------------------------------------------------------");
        Console.WriteLine($"Price: {price} kr");
        Console.WriteLine($"VAT amount: {totalPrice - price} kr");
        Console.WriteLine($"Total: {totalPrice} kr");
    }
}