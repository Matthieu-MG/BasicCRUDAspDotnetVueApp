namespace Enterprise.Models.Responses;

public record class OrderDTO
{
    //* PK
    public int ID { get; set; }
    
    //* FK
    public string ProductName { get; set; } = default!;
    public int ProductId { get; set; }

    //* Should be > 0
    public int Units { get; set; }

    public string Price { get; set; } = default!;
    
    //* FK
    public string SocietyName { get; set; } = default!;
    public int SocietyId { get; set; }
    
    public DateOnly DateOrdered { get; set; }
    public DateOnly ExpectedDeliveryDate {get; set;}
    
    public string State {get; set;} = default!;

    public string ShippingAddress { get; set; } = default!;

    //* FK
    public string EmployeeName { get; set; } = default!;
    public int EmployeeId { get; set; }
}