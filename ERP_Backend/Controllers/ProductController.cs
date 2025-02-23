using Enterprise.API.DTOs;
using Enterprise.API.Product;
using Enterprise.API.ProductDTO;
using Microsoft.AspNetCore.Mvc;
using Enterprise.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : GenericController<Product, PostProductDTO, ProductDTO>, IMetadataProvider
{
    private readonly ProductRepository _productService;

    public ProductController(ProductRepository productService) : base(productService)
    {
        _productService = productService;
    }

    [HttpGet("PostDTO")]
    public ActionResult<List<PostDTOMetaData>> GetPostDTOMetaData()
    {
        return Ok( new List<PostDTOMetaData> {
            new() { Name = "Name", Type = "string", Label = "Name" },
            new() { Name = "StandardPrice", Type = "price", Label = "Standard Price" }
        });
    }

    [HttpGet("Names/{name}")]
    public async Task<ActionResult> GetProductsByNames(string name)
    {
        return Ok(await _productService.GetByName(name));
    }

    [HttpGet("SortOptions")]
    public ActionResult<List<string>> GetSortOptions()
    {
        return Ok(new List<string>{
            "Name",
            "Price",
        });
    }
}