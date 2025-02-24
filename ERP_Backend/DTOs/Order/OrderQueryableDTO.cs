namespace Enterprise.Models.Responses;

//* Main purpose is for EF to use when doing queries to db, since price can't be formatted to "C2"
//* and cause an exception since this mapping requires .NET operation not SQL
public record class OrderQueryableDTO
{
    //* PK
    public int ID { get; set; }
    
    //* FK
    public int ProductId { get; set; }
    public string ProductName { get; set; } = default!;

    //* Should be > 0
    public int Units { get; set; }

    public decimal Price { get; set; }
    
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