namespace Vega.Extensions
{
    public interface IQueryObject
    { 
        string SortBy { get; set; }
        bool IsSortAsc { get; set; }

        int Page { get; set; }
        int PageSize { get; set; }
    }
}