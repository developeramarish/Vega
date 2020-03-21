namespace Vega.Controllers.Resources
{
    public class VehicleQueryResource
    {
        public int? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAsc { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}