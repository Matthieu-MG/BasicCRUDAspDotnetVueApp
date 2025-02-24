public interface IGenericRepository<TEntity, TPost, TGet>
    where TEntity:class
    where TPost: class
    where TGet:class
{
    public Task<int> GetTotalRecordsCount();

    public Task Create(TPost postDTO);

    public Task<List<TGet>> Read();

    public Task<TGet?> ReadById(int id);

    public Task Update(int id, TPost updateDTO);

    public Task Delete(int id);
}