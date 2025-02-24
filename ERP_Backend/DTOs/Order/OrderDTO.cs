using Enterprise.Enums;
namespace Enterprise.Models.Responses;

public record class OrderDTO
{
    //* PK
    public int ID { get; set; }
    
    //* FK
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;

    //* Should be > 0
    public int Units { get; set; }

    public string Price { get; set; } = default!;
    
    //* FK
    public int SocietyId { get; set; }
    public string SocietyName { get; set; } = default!;
    
    public DateOnly DateOrdered { get; set; }
    public DateOnly ExpectedDeliveryDate {get; set;}
    
    public string State {get; set;} = default!;

    public string ShippingAddress { get; set; } = default!;

    //* FK
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
}