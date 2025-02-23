namespace Enterprise.Models.Requests;

public record class PostSocietyDTO
{
    public string Name {get; set;} = null!;
    public string? FullName {get; set;}
    public string PostalCode {get; set;} = null!;
    public string Town {get; set;} = null!;
    public string Country {get; set;} = null!;
}