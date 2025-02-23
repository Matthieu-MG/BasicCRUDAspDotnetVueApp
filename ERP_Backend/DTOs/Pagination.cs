using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Enterprise.API.Requests;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.API.Responses;

public record class Pagination<T>(
    List<T> Items,
    int Page,
    int ItemsPerPage,
    int TotalItems
)
{

    public bool HasNextPage => Page * ItemsPerPage < TotalItems;
    public bool HasPreviousPage => Page > 1;

    public static async Task<Pagination<T>> CreateAsync(IQueryable<T> query, int page, int itemsPerPage)
    {
        int totalItems = await query.CountAsync();
        List<T> items = await query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();

        return new(items, page, itemsPerPage, totalItems);
    }

    //* T : GetDTO Class, M : Model Data Class
    public static async Task<Pagination<T>> GetPage<M>(
      GetQueryDTO request, IQueryable<M> query,
      Func< IQueryable<M>, IQueryable<M> > filterFunc,
      Func<GetQueryDTO, Expression<Func<M, object>> > sortProperty,
      AutoMapper.IConfigurationProvider configurationProvider
    )
    {
        if(!request.IsUsingPagination)
        {
            throw new NullReferenceException();
        }

        if(filterFunc == null || sortProperty == null)
        {
            throw new NullReferenceException();
        }

        //* Filtering
        if(!string.IsNullOrEmpty(request.SearchTerm))
        {
            query = filterFunc(query);
        }

        //* Sorting
        if(request.IsDescending)
        {
            query = query.OrderByDescending(sortProperty(request));
        }
        else
        {
            query = query.OrderBy(sortProperty(request));
        }

        //* Project to DTO Properties
        var DTOQuery = query.ProjectTo<T>(configurationProvider);

        //* Pageing
        var page = await Pagination<T>.CreateAsync(
            DTOQuery, 
            request.Page, 
            request.ItemsPerPage
        );

        return page;
    }
}