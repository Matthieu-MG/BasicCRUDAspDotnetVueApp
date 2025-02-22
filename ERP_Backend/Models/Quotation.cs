using Enterprise.API.Employee;
using Enterprise.API.Product;
using Enterprise.API.Society;

namespace MvcQuotation.Models;

public class Quotation
{
    //* PK
    public int ID { get; set; }
    
    //* FK
    public int ProductID { get; set; }
    public Product ProductObj { get; set; } = null!;

    //* Should be > 0
    public int Units { get; set; }

    public decimal Price { get; set; }
    
    //* FK
    public int SocietyID { get; set; }
    public Society SocietyObj { get; set; } = null!;
    
    public QuotationState State {get; set;} = QuotationState.Drafted;
    
    //* FK
    public int EmployeeID { get; set; }
    public Employee EmployeeObj { get; set; } = null!;
}