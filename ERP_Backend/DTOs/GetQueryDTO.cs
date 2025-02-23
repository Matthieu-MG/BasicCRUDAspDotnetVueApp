namespace Enterprise.API.Requests;

public record class GetQueryDTO
{
    public string? SearchTerm {get; set;}
    public string? SortOrder {get; set;}
    public string? SortBy {get; set;}

    public int Page {get; set;} = 1;
    public int ItemsPerPage {get; set;} = 10;

    //* Whether SortOrder is descending (true) or ascending (false)
    public bool IsDescending => SortOrder != null && SortOrder.ToLower().Contains("desc");

    //* Whether a search was queried
    public bool IsSearchTermAssigned => !string.IsNullOrEmpty(SearchTerm);

    //* Whether request is using pagination
    public bool IsUsingPagination => Page > 0 && ItemsPerPage > 0;
}