namespace Enterprise.Models.Requests;

public record class PostProductDTO
{
    //* Product Name --TODO Switch to required, upgrade C#
    public string Name { get; set; } = null!;
    //* Base Price for that Product
    public decimal StandardPrice {get; set;}
}