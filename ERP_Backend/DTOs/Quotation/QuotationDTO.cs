namespace Enterprise.API.QuotationDTO;

public record class QuotationDTO
{
    //* PK
    public int ID { get; set; }
    //* FK
    public string ProductName { get; set; } = null!;
    public int ProductID { get; set; }
    
    //* Should be > 0
    public int Units { get; set; }
    
    //* FK
    public string SocietyName { get; set; } = null!;
    public int SocietyId {get; set;}

    public string State {get; set;} = QuotationState.Drafted.ToString();

    //* FK
    public string EmployeeName { get; set; } = null!;
    public int EmployeeId {get; set;}
}