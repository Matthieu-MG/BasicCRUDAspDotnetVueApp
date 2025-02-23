namespace Enterprise.Models.Responses;

//TODO Add Employee Department Field
public record class EmployeeDTO
{
    public int ID {get; set;}
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Mobile { get; set; } = null!;
}