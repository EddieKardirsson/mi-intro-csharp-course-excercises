namespace RipOffShopOnline;

public static class StoreManager
{
    public static List<Product> Products { get; set; }
    
    private static int _productListCounter = 0;
    
    public static decimal CalculateTotalPrice(List<Product> products) => products.Sum(p => p.Price);
    
    public static decimal CalculateTotalPriceWithVat(List<Product> products) => products.Sum(p => p.PriceWithVat);

    public static void Session()
    {
        // - Create a list of products
        Products = ProductsGenerator.GenerateProducts();
        
        // TODO: Initialize Shopping Cart
    }
    
    public static int IncrementProductListCounter() => ++_productListCounter;
    
}