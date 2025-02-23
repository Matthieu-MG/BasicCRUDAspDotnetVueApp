namespace Enterprise.Models.Responses;

//* DTO for most/least quoted items, to represent on charts for most cases
public record class QuotedProductDTO
{
    public string ProductName { get; set;} = default!;
    public decimal TotalRevenue {get; set;}
}