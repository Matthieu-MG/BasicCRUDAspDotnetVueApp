namespace Enterprise.API.ProductDTO;

public record class ProductDTO
{
    //* PK
    public int Id { get; set; }
    //* Product Name --TODO Switch to required, upgrade C#
    public string Name { get; set; } = null!;
    //* Base Price for that Product
    public decimal StandardPrice {get; set;}
}