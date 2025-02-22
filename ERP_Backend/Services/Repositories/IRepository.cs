public interface IRepository<TGet, TPost>
{
    public Task<List<TGet>> Get();
    public Task<TGet?> GetById(int id);
    public Task Post(TPost postContent);
    public Task Update(int id, TPost postContent);
    public Task Delete(int id);
}