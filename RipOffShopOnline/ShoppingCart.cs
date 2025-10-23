namespace RipOffShopOnline;

public class ShoppingCart
{
    List<Product> Products { get; set; }

    public ShoppingCart()
    {
        Products = new List<Product>();
    }
    
    public void AddToCart(Product product) => Products.Add(product);

    public void RemoveFromCart(Product product) => Products.Remove(product);

    public decimal CalculateTotalPrice() => StoreManager.CalculateTotalPrice(Products);
    public decimal CalculateTotalPriceWithVat() => StoreManager.CalculateTotalPriceWithVat(Products);
}