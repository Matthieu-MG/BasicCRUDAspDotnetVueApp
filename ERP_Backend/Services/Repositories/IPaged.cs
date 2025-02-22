using Enterprise.API.GetQueryDTO;
using Enterprise.API.Pagination;

public interface IPaged<T>
{
    public Task<Pagination<T>> GetPage(GetQueryDTO request);
}