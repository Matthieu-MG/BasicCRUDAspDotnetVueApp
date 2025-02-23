using System.Data;
using System.Linq.Expressions;
using AutoMapper;

using Enterprise.Data;
using Microsoft.EntityFrameworkCore;
using Enterprise.API.Services.Repositories;
using Enterprise.Models;
using Enterprise.Models.Requests;
using Enterprise.Models.Responses;
using Enterprise.API.Responses;
using Enterprise.API.Requests;

public class ProductRepository : 
    GenericRepository<Product, PostProductDTO, ProductDTO>,
    IPagedGenericRepository<Product, PostProductDTO, ProductDTO>
{

    public ProductRepository(EnterpriseDbContext context, IMapper mapper) : base(context, mapper)
    {}

    public async Task<Pagination<ProductDTO>> GetPage(GetQueryDTO request)
    {
        return await Pagination<ProductDTO>.GetPage<Product>(
            request, _context.Products,
            (q) => q.Where(p => p.Name.Contains(request.SearchTerm ?? "")),
            GetSortProperty,
            _mapper.ConfigurationProvider
        );
    }

    public async Task<List<ProductDTO>> GetByName(string name)
    {
        var societies = await _context.Products
                            .Where(s => s.Name.Contains(name))
                            .ToListAsync();

        return _mapper.Map<List<ProductDTO>>(societies);
    }

    public async Task<bool> IsProductNameUnique(string name)
    {
        bool r = !await _context.Products.AnyAsync(p => p.Name == name);
        return r;
    }

    private Expression<Func<Product, object>> GetSortProperty(GetQueryDTO request)
    {
        return request.SortBy?.ToLower() switch
        {
          "name"  => product => product.Name,
          "price" => product => product.StandardPrice,
          _ => product => product.Id
        };
    }
}