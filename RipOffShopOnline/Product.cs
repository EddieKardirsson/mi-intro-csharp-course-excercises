namespace RipOffShopOnline;

public class Product
{
    private int Id { get; }
    public string? Name { get; }
    public ProductType Type { get; }
    public decimal Price { get; }
    public int Quantity { get; private set; }
    private decimal Vat { get; }
    public decimal PriceWithVat => Price + (Price * Vat);
    
    private const decimal BOOK_VAT = 0.06m;
    private const decimal STANDARD_VAT = 0.25m;
    
    public Product(string name, ProductType type, decimal price, int quantity)
    {
        Id = StoreManager.IncrementProductListCounter();
        Name = name;
        Type = type;
        Price = price;
        Quantity = quantity;
        Vat = type == ProductType.Book ? BOOK_VAT : STANDARD_VAT;
    }
}

public enum ProductType
{
    Book,
    Electronics,
    Furniture,
    Clothing,
    Other
}

public static class ProductsGenerator
{
    public static List<Product> Products { get; set; }
    
    public static List<Product> GenerateProducts()
    {
        Products = new List<Product>
        {
            new Product("Dave Lowrell's 1969", ProductType.Book, 299m, 100),
            new Product("Sumthing Blackhole G25 Smartphone 128 GB", ProductType.Electronics, 5990m, 50),
            new Product("ÖKEA 4-seat Sofa ", ProductType.Furniture, 9990m, 20),
            new Product("Jake & John's Denim Jeans", ProductType.Clothing, 699m, 200),
            new Product("Depresso Cottage Coffee Mug", ProductType.Other, 69.90m, 150),
            new Product("Mikael Mikael Sheep Plushy", ProductType.Other, 499m, 40)
        };

        return Products;
    }
    
}