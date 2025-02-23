using Enterprise.Enums;
namespace Enterprise.Models.Responses;

public record class QuotationDTO
{
    //* PK
    public int ID { get; set; }
    //* FK
    public string ProductName { get; set; } = default!;
    public int ProductID { get; set; }
    
    //* Should be > 0
    public int Units { get; set; }
    public string Price {get; set;} = default!;
    
    //* FK
    public string SocietyName { get; set; } = default!;
    public int SocietyId {get; set;}

    public string State {get; set;} = QuotationState.Drafted.ToString();

    //* FK
    public string EmployeeName { get; set; } = default!;
    public int EmployeeId {get; set;}
}