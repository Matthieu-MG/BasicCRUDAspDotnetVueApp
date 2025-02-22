namespace Enterprise.API.PostQuotationDTO;

public record class PostQuotationDTO
{
    //* FK
    public int ProductID { get; set; }
    //* Should be > 0
    public int Units { get; set; }
    //* FK
    public int SocietyID { get; set; }

    public QuotationState State {get; set;}

    //* FK
    public int EmployeeID { get; set; }
}