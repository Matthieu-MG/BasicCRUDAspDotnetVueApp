namespace Enterprise.Models;

public class Product
{
    //* PK
    public int Id { get; set; }
    //* Product Name --TODO Switch to required, upgrade C#
    public string Name { get; set; } = null!;
    //* Base Price for that Product
    public decimal StandardPrice {get; set;}

    public override string ToString()
    {
        return $"Product: Id={Id}, Name={Name}, StandardPrice={StandardPrice}";
    }
}