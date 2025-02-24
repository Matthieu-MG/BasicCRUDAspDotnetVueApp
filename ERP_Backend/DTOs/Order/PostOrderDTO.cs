using Enterprise.Enums;
namespace Enterprise.Models.Requests;

public record class PostOrderDTO
{
    //* FK
    public int ProductId { get; set; }

    //* Should be > 0
    public int Units { get; set; }

    public decimal Price { get; set; }
    
    //* FK
    public int SocietyId { get; set; }
    
    public DateOnly DateOrdered { get; set; }
    public DateOnly ExpectedDeliveryDate {get; set;}
    
    public OrderState State {get; set;}

    public string ShippingAddress { get; set; } = default!;

    //* FK
    public int EmployeeId { get; set; }
}