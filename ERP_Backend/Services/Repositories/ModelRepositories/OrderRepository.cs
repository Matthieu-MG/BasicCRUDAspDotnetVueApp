using Enterprise.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Enterprise.API.Services.Repositories;
using Enterprise.Models.Requests;
using Enterprise.Models.Responses;
using Enterprise.Models;
using Enterprise.API.Responses;
using Enterprise.API.Requests;

namespace Enterprise.Services.Repositories;

public class OrderRepository : GenericRepository<Order, PostOrderDTO, OrderDTO>,
    IPagedGenericRepository<Order, PostOrderDTO, OrderDTO>
{

    public OrderRepository(EnterpriseDbContext context, IMapper mapper) : base(context, mapper)
    {}
    public async Task<Pagination<OrderDTO>> GetPage(GetQueryDTO request)
    {
        return await Pagination<OrderDTO>.GetPage<Order>(request, _context.Order,
            (q) => q.Where( (o) => o.ProductObj.Name.Contains(request.SearchTerm ?? "") ),
            GetSortProperty,
            _mapper.ConfigurationProvider
        );
    }

    private Expression<Func<OrderDTO, object>> GetSortProperty(GetQueryDTO request)
    {
        return request.SortBy?.ToLower() switch
        {
            "product" => o => o.ProductName,
            "employee" => o => o.EmployeeName,
            "society" => o => o.SocietyName,
            "dateordered" => o => o.DateOrdered,
            "units" => o => o.Units,
            "price" => o => o.Price,
            _ => o => o.ID
        };
    }
}