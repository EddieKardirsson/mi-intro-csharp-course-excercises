namespace RipOffShopOnline;

public class Product
{
    private int Id { get; }
    public string? Name { get; }
    public ProductType Type { get; }
    public decimal Price { get; }
    public int Quantity { get; set; }
    private decimal Vat { get; }
    public decimal PriceWithVat => Price + (Price * Vat);
    
    private decimal _purchasePrice;
    
    private const decimal BOOK_VAT = 0.06m;
    private const decimal STANDARD_VAT = 0.25m;
    
    public Product(string name, ProductType type, decimal price, int quantity, decimal purchasePrice)
    {
        Id = StoreManager.IncrementProductListCounter();
        Name = name;
        Type = type;
        Price = price;
        Quantity = quantity;
        _purchasePrice = purchasePrice;
        Vat = type == ProductType.Book ? BOOK_VAT : STANDARD_VAT;
    }
    
    public void DecrementQuantity() => Quantity--;
    
    public decimal GetPurchasePrice() => _purchasePrice = Price;
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
            new Product("Dave Lowrell's 1969", ProductType.Book, 299m, 100, 80m),
            new Product("Sumthing Blackhole G25 Smartphone 128 GB", ProductType.Electronics, 5990m, 50, 2000),
            new Product("ÖKEA 4-seat Sofa ", ProductType.Furniture, 9990m, 20, 4000),
            new Product("Jake & John's Denim Jeans", ProductType.Clothing, 699m, 200, 300m),
            new Product("Depresso Cottage Coffee Mug", ProductType.Other, 69.90m, 150, 20m),
            new Product("Mikael Mikael Sheep Plushy", ProductType.Other, 499m, 40, 130m)
        };

        return Products;
    }
    
}