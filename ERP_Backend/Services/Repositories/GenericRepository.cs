using AutoMapper;
using Enterprise.Data;
using Microsoft.EntityFrameworkCore;

public class GenericRepository<TEntity, TPost, TGet> :
    IGenericRepository<TEntity, TPost, TGet>
    where TEntity:class
    where TGet: class
    where TPost:class
{
    protected readonly EnterpriseDbContext _context;
    protected readonly IMapper _mapper;

    public GenericRepository(EnterpriseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> GetTotalRecordsCount()
    {
        return await _context.Set<TEntity>().CountAsync();
    }

    public async Task Create(TPost postDTO)
    {
        await _context.Set<TEntity>().AddAsync(_mapper.Map<TEntity>(postDTO));
        await _context.SaveChangesAsync();
    }

    public async Task<List<TGet>> Read()
    {
        var entities = await _context.Set<TEntity>().ToListAsync();
        return _mapper.Map<List<TGet>>(entities);
    }

    public async Task<TGet?> ReadById(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if(entity == null)
        {
            return null;
        }
        return _mapper.Map<TGet>(entity);
    }

    public async Task Update(int id, TPost updateDTO)
    {
        try
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _mapper.Map(updateDTO, entity);
                Console.WriteLine(entity);
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
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        catch (DbUpdateConcurrencyException e)
        {
            // TODO: Handle that exception, in case a user tries to delete a record that's already been/being deleted !
            Console.WriteLine(e);
        }
    }
}