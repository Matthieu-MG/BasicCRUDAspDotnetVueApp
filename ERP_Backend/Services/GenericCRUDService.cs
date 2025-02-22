using AutoMapper;
using Enterprise.Data;
using Microsoft.EntityFrameworkCore;

public class GenericCRUDService<TModel, TGetDto, TPostDto>
 where TModel : class
 where TGetDto : class
 where TPostDto : class
{
    private readonly EnterpriseDbContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<TModel> _dbSet;

    public GenericCRUDService(EnterpriseDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<TModel>();
        _mapper = mapper;
    }

    public async Task<List<TGetDto>> Get()
    {
        var records = await _dbSet.ToListAsync();
        return _mapper.Map<List<TGetDto>>(records);
    }

    public async Task<TGetDto?> GetById(int id)
    {
        var record = await _dbSet.FindAsync(id);
        if (record == null)
        {
            return null;
        }
        return _mapper.Map<TGetDto>(record);
    }

    public async Task Add(TPostDto postDto)
    {
        await _dbSet.AddAsync(_mapper.Map<TModel>(postDto));
        await _context.SaveChangesAsync();
    }
}