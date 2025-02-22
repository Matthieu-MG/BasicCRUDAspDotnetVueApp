using Enterprise.Data;
using Microsoft.EntityFrameworkCore;
using MvcQuotation.Models;
using AutoMapper;
using Enterprise.API.QuotationDTO;
using Enterprise.API.PostQuotationDTO;
using Enterprise.API.GetQueryDTO;
using Enterprise.API.Pagination;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Enterprise.API.Services.Repositories;

public class QuotationService : IPagedGenericRepository<Quotation, PostQuotationDTO, QuotationDTO>
{
    private readonly EnterpriseDbContext _context;
    private readonly IMapper _mapper;

    public QuotationService(EnterpriseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //* Fetches All Quotations from ORM
    public async Task<List<QuotationDTO>> Read()
    {
        var model = await _context.Quotations
                        .Include(q => q.ProductObj)
                        .Include(q => q.EmployeeObj)
                        .Include(q => q.SocietyObj)
                        .ToListAsync();
                        
        return _mapper.Map<List<QuotationDTO>>(model);
    }

    public async Task<Pagination<QuotationDTO>> GetPage(GetQueryDTO request)
    {
        if(!request.IsUsingPagination)
        {
            throw new NullReferenceException();
        }

        Console.WriteLine(request);
        IQueryable<Quotation> query = _context.Quotations;

        //* Filtering
        if(!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = query.Include(quote => quote.ProductObj).Where( quote => quote.ProductObj.Name.Contains(request.SearchTerm ?? ""));
        }

        //* Sorting
        if(request.IsDescending)
        {
            query = query.Include(q => q.ProductObj)
                         .Include(q => q.SocietyObj)
                         .Include(q => q.EmployeeObj)
                         .OrderByDescending(GetSortProperty(request));
        }
        else
        {
            query = query.Include(q => q.ProductObj)
                         .Include(q => q.SocietyObj)
                         .Include(q => q.EmployeeObj)
                         .OrderBy(GetSortProperty(request));
        }

        //* Project to DTO Properties
        IQueryable<QuotationDTO> DTOQuery = query.ProjectTo<QuotationDTO>(_mapper.ConfigurationProvider);

        //* Pageing
        var page = await Pagination<QuotationDTO>.CreateAsync(
            DTOQuery, 
            request.Page, 
            request.ItemsPerPage
        );

        return page;
    }

    //* Fetches Quotation by ID from ORM
    public async Task<QuotationDTO?> ReadById(int id)
    {
        var model =  await _context.Quotations
                    .Include(q => q.ProductObj)
                    .Include(q => q.EmployeeObj)
                    .Include(q => q.SocietyObj)
                    .FirstOrDefaultAsync(q => q.ID == id);
        if (model == null)
        {
            return null;
        }
        return _mapper.Map<QuotationDTO>(model);
    }

    //* Add Quote to table
    public async Task Create(PostQuotationDTO quotation)
    {
        await _context.Quotations.AddAsync(_mapper.Map<Quotation>(quotation));
        await _context.SaveChangesAsync();
    }

    private Expression<Func<Quotation, object>> GetSortProperty(GetQueryDTO request)
    {
        return request.SortBy?.ToLower() switch
        {
            "product" => q => q.ProductObj.Name,
            "employee" => q => q.EmployeeObj.Name,
            "society" => q => q.SocietyObj.Name,
            "units" => q => q.Units,
            _ => q => q.ID
        };
    }

    public async Task Update(int id, PostQuotationDTO postContent)
    {        
        try
        {
            var quotation = await _context.Quotations.FirstOrDefaultAsync(e => e.ID == id);
            if (quotation != null)
            {
                _mapper.Map(postContent, quotation);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbUpdateConcurrencyException e)
        {
            //TODO Properly handle that exception
            Console.WriteLine(e);
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var quotation =_context.Attach(new Quotation {ID = id});
            quotation.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            // TODO: Handle that exception, in case a user tries to delete a record that's already been/being deleted !
            Console.WriteLine(e);
        }
    }

    public int GetTotalRecordsCount()
    {
        return _context.Quotations.Count();
    }
}