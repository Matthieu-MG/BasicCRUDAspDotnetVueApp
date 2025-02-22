namespace Enterprise.API.Services.Repositories;

public interface IPagedGenericRepository<TEntity, TPost, TGet> :
    IGenericRepository<TEntity, TPost, TGet> , 
    IPaged<TGet>
    where TEntity : class
    where TPost : class
    where TGet : class
{}