namespace RipOffShopOnline;

public class ShoppingCart
{
    List<Product> ProductsInCart { get; }

    public ShoppingCart()
    {
        ProductsInCart = new List<Product>();
    }
    
    public void AddToCart(Product product) => ProductsInCart.Add(product);

    public void RemoveFromCart(Product product) => ProductsInCart.Remove(product);

    public decimal CalculateTotalPrice() => StoreManager.CalculateTotalPrice(ProductsInCart);
    public decimal CalculateTotalPriceWithVat() => StoreManager.CalculateTotalPriceWithVat(ProductsInCart);
}