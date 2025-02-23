using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Enterprise.API.Controllers;
using Enterprise.Models.Requests;
using Enterprise.Models;
using Enterprise.Models.Responses;
using Enterprise.API.Responses;

[ApiController]
[Route("[controller]")]
public class SocietyController : GenericController<Society, PostSocietyDTO, SocietyDTO>, IMetadataProvider
{
    private readonly SocietyRepository _societyService;

    public SocietyController(SocietyRepository societyService) : base(societyService)
    {
        _societyService = societyService;
    }

    [HttpGet("PostDTO")]
    public ActionResult<List<PostDTOMetaData>> GetPostDTOMetaData()
    {
        return Ok( new List<PostDTOMetaData>{
            new() { Name = "Name", Type = "string", Label = "Name"},
            new() { Name = "FullName", Type = "string", Label = "Full Name"},
            new() { Name = "PostalCode", Type = "int", Label = "Postal Code"},
            new() { Name = "Town", Type = "string", Label = "Town"},
            new() { Name = "Country", Type = "string", Label = "Country"}
        });
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetSocietiesByName(string name)
    {
        var societies = await _societyService.GetByName(name);
        // What to do if societies is empyty ?
        return Ok(societies);
    }

    [HttpGet("SortOptions")]
    public ActionResult<List<string>> GetSortOptions()
    {
        return Ok(new List<string>{
            "Name",
            "Town",
            "Country"
        });
    }
}