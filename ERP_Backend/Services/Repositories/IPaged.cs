using Enterprise.API.Requests;
using Enterprise.API.Responses;

public interface IPaged<T>
{
    public Task<Pagination<T>> GetPage(GetQueryDTO request);
}