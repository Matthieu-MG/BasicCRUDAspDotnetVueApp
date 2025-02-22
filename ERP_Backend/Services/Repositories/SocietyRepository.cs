using Enterprise.API.PostSocietyDTO;
using Enterprise.API.SocietyDTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Enterprise.API.Society;
using Enterprise.Data;
using Enterprise.API.Pagination;
using Enterprise.API.GetQueryDTO;
using System.Linq.Expressions;
using Enterprise.API.Services.Repositories;

public class SocietyRepository : 
    GenericRepository<Society, PostSocietyDTO, SocietyDTO>, 
    IPagedGenericRepository<Society, PostSocietyDTO, SocietyDTO>
{
    public SocietyRepository(EnterpriseDbContext context, IMapper mapper) : base(context, mapper)
    {}

    public async Task<Pagination<SocietyDTO>> GetPage(GetQueryDTO request)
    {
        return await Pagination<SocietyDTO>.GetPage<Society>(
            request, _context.Society,
            (q) => q.Where(s => s.Name.Contains(request.SearchTerm ?? "")),
            GetSortProperty,
            _mapper.ConfigurationProvider);
    }

    public async Task<List<SocietyDTO>> GetByName(string name)
    {
        var societies = await _context.Society
                            .Where(s => s.Name.Contains(name) || (s.FullName != null && s.FullName.Contains(name)))
                            .ToListAsync();

        return _mapper.Map<List<SocietyDTO>>(societies);
    }

    public async Task<bool> IsSocietyFullNameUnique(string name)
    {
        return !await _context.Society.AnyAsync(s => s.FullName == name);
    }

    private Expression<Func<Society, object>> GetSortProperty(GetQueryDTO request)
    {
        return request.SortBy?.ToLower() switch
        {
            "name" => society => society.Name,
            "town" => society => society.Town,
            "country" => society => society.Country,
            _ => society => society.ID
        };
    }
}