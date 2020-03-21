namespace Vega.Core.Models
{
    public class VehicleQuery
    {
        public int? MakeId { get; set; }

        public string SortBy { get; set; }
        public bool IsSortAsc { get; set; }
    }
}