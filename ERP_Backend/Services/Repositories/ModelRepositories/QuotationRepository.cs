using System.Linq.Expressions;
using AutoMapper;
using Enterprise.API.Requests;
using Enterprise.API.Responses;
using Enterprise.API.Services.Repositories;
using Enterprise.Data;
using Enterprise.Enums;
using Enterprise.Models;
using Enterprise.Models.Requests;
using Enterprise.Models.Responses;
using Microsoft.EntityFrameworkCore;

public class QuotationRepository : GenericRepository<Quotation, PostQuotationDTO, QuotationDTO>,
    IPagedGenericRepository<Quotation, PostQuotationDTO, QuotationDTO>
{

    public QuotationRepository(EnterpriseDbContext _context, IMapper _mapper) : base(_context, _mapper)
    {}

    public async Task<Pagination<QuotationDTO>> GetPage(GetQueryDTO request)
    {
        return await Pagination<QuotationDTO>.GetPage<Quotation>(request, _context.Quotations,
            (query) => query.Where( quote => quote.ProductObj.Name.Contains(request.SearchTerm ?? "")),
            GetSortProperty,
            _mapper.ConfigurationProvider
        );
    }

    //* Used by client to represent most/least quoted products on charts
    public async Task<List<QuotedProductDTO>> GetQuotedProductsRanking(string opt)
    {
        const int MAX = 5;
        var query = _context.Quotations.Where( q => q.State == QuotationState.ConvertedToOrder )
                                       .GroupBy( q => q.ProductID)
                                       .Select(q => new
                                       {
                                            ProductId = q.Key,
                                            QuotedCount = q.Sum( q => q.Price)
                                       });

        if(query == null)
        {
            throw new NullReferenceException();
        }

        if(opt != null && opt.Contains("desc"))
        {
            query = query.OrderByDescending(g => g.QuotedCount);
        }
        else
        {
            query = query.OrderBy(g => g.QuotedCount);
        }

        return await query.Take(MAX)
            .Join(_context.Products, g => g.ProductId, p => p.Id, (g, p) => new QuotedProductDTO
            {
                ProductName = p.Name,
                TotalRevenue = g.QuotedCount
            })
            .ToListAsync();
    }

    private Expression<Func<QuotationDTO, object>> GetSortProperty(GetQueryDTO request)
    {
        return request.SortBy?.ToLower() switch
        {
            "product" => q => q.ProductName,
            "employee" => q => q.EmployeeName,
            "society" => q => q.SocietyName,
            "units" => q => q.Units,
            "price" => q => q.Price,
            _ => q => q.ID
        };
    }
}