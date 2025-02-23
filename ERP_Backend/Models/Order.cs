using Enterprise.Enums;
namespace Enterprise.Models;

public class Order
{
    //* PK
    public int ID { get; set; }
    
    //* FK
    public int ProductID { get; set; }
    public Product ProductObj { get; set; } = default!;

    //* Should be > 0
    public int Units { get; set; }

    public decimal Price { get; set; }
    
    //* FK
    public int SocietyID { get; set; }
    public Society SocietyObj { get; set; } = default!;
    
    public DateOnly DateOrdered { get; set; }
    public DateOnly ExpectedDeliveryDate {get; set;}
    
    public OrderState State {get; set;}

    public string ShippingAddress { get; set; } = default!;

    //* FK
    public int EmployeeID { get; set; }
    public Employee EmployeeObj { get; set; } = default!;
}