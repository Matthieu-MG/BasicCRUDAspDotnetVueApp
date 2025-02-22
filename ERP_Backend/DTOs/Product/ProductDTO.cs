namespace Enterprise.API.ProductDTO;

public record class ProductDTO
{
    //* PK
    public int Id { get; set; }
    //* Product Name --TODO Switch to required, upgrade C#
    public string Name { get; set; } = default!;
    //* Base Price for that Product
    public string StandardPrice {get; set;} = default!;
}