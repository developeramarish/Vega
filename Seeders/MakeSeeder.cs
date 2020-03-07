using System.Collections.Generic;
using System.Linq;
using Vega.Models;
using Vega.Persistence;

namespace Vega.Seeders
{
    public static class MakeSeeder
    {
        public static void SeedMakes(VegaDbContext context)
        {
            var makes = new List<Make>
            {
                new Make { Name = "Make1" },
                new Make { Name = "Make2" }
            };

            if (!context.Makes.Any())
            {
                context.Makes.AddRange(makes);
                context.SaveChanges();
            }

        }
    }
}