namespace Enterprise.Models;

public class Society
{
    public int ID {get; set;}
    public string Name {get; set;} = null!;
    public string? FullName {get; set;}
    public string PostalCode {get; set;} = null!;
    public string Town {get; set;} = null!;
    public string Country {get; set;} = null!;
}