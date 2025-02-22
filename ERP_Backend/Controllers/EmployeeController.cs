using Enterprise.API.Employee;
using Enterprise.API.EmployeeDTO;
using Enterprise.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Enterprise.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : GenericController<Employee, PostEmployeeDTO, EmployeeDTO>, IMetadataProvider
{
    private readonly EmployeeRepository _employeeService;

    public EmployeeController(EmployeeRepository employeeService) : base(employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("names/{name}")]
    public async Task<ActionResult> GetEmployeesByName(string name)
    {
        var employees = await _employeeService.GetEmployeesByName(name);
        return Ok(employees);
    }

    [HttpGet("PostDTO")]
    public ActionResult<List<PostDTOMetaData>> GetPostDTOMetaData()
    {
        return Ok( new List<PostDTOMetaData> {
            new() { Name = "Surname", Type = "string", Label = "Surname"},
            new() { Name = "Name", Type = "string", Label = "Name"},
            new() { Name = "DateOfBirth", Type = "date", Label = "Date of Birth"},
            new() { Name = "Address", Type = "string", Label = "Address"},
            new() { Name = "Email", Type = "string", Label = "Email"},
            new() { Name = "Mobile", Type = "int", Label = "Mobile"}
        });
    }

    [HttpGet("SortOptions")]
    public ActionResult<List<string>> GetSortOptions()
    {
        return Ok(new List<string>{
            "Name",
            "Surname",
            "DOB"
        });
    }
}