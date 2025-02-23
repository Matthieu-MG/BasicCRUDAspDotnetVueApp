using Microsoft.AspNetCore.Mvc;
using Enterprise.API.QuotationDTO;
using Enterprise.API.PostQuotationDTO;
using Enterprise.API.Controllers;
using MvcQuotation.Models;
using Enterprise.API.DTOs;

[ApiController]
[Route("[controller]")]
public class QuotationController : GenericController<Quotation, PostQuotationDTO, QuotationDTO>, IMetadataProvider
{
    private readonly QuotationService _quotationService;

    public QuotationController(QuotationService quotationService) : base(quotationService)
    {
        _quotationService = quotationService;
    }

    [HttpGet("QuotationStates")]
    public ActionResult<List<EnumDTO>> GetQuotationStates()
    {
        const int MAX_STATES = (int)QuotationState.ConvertedToOrder + 1;
        List<EnumDTO> states = new(MAX_STATES);

        for (int i = 0; i < MAX_STATES; i++)
        {
            states.Add(new() { Name = ((QuotationState)i).ToString(), Value = i });
        }

        return Ok(states);
    }

    [HttpGet("PostDTO")]
    public ActionResult<List<PostDTOMetaData>> GetPostDTOMetaData()
    {
        return Ok( new List<PostDTOMetaData> {
            new() { Name = "ProductID", Type = "foreign", Label = "Product Name", Route="Product/Names/"},
            new() { Name = "Units", Type = "int", Label = "Units"},
            new() { Name = "Price", Type = "price", Label = "Price"},
            new() { Name = "SocietyID", Type = "foreign", Label = "Society Name", Route="Society/Name/"},
            new() { Name = "State", Type = "enum", Label = "State", Route = "Quotation/QuotationStates"},
            new() { Name = "EmployeeID", Type = "foreign", Label = "Employee Name", Route="Employee/Names/"}
        });
    }

    [HttpGet("SortOptions")]
    public ActionResult<List<string>> GetSortOptions()
    {
        return Ok(new List<string>{
            "Product",
            "Employee",
            "Society",
            "Units",
            "Price"
        });
    }

    [HttpGet("Ranking/{opt}")]
    public async Task<ActionResult> GetQuotes(string opt)
    {
        return Ok(await _quotationService.GetQuotedProductsRanking(opt));
    }
}