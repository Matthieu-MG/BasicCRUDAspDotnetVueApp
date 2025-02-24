using Enterprise.API.Controllers;
using Enterprise.API.Responses;
using Enterprise.Enums;
using Enterprise.Models;
using Enterprise.Models.Requests;
using Enterprise.Models.Responses;
using Enterprise.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : GenericController<Order, PostOrderDTO, OrderDTO>
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository) : base(orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("OrderStates")]
    public ActionResult<List<EnumDTO>> GetQuotationStates()
    {
        const int MAX_STATES = (int)OrderState.Canceled+ 1;
        List<EnumDTO> states = new(MAX_STATES);

        for (int i = 0; i < MAX_STATES; i++)
        {
            states.Add(new() { Name = ((OrderState)i).ToString(), Value = i });
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
            new() { Name = "DateOrdered", Type = "date", Label = "Date Ordered"},
            new() { Name = "ExpectedDeliveryDate", Type = "date", Label = "Expected Delivery Date"},
            new() { Name = "State", Type = "enum", Label = "Order State", Route = "Order/OrderStates"},
            new() { Name = "ShippingAddress", Type = "string", Label = "Shipping Address"},
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
            "DateOrdered",
            "Units",
            "Price"
        });
    }
}