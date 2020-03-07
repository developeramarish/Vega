using System.Collections.Generic;
using System.Linq;
using Vega.Models;
using Vega.Persistence;

namespace Vega.Seeders
{
    public class FeatureSeeder
    {
        public static void SeedFeatures(VegaDbContext context)
        {
            var features = new List<Feature>
            {
                new Feature { Name = "Feature1" },
                new Feature { Name = "Feature2" },
                new Feature { Name = "Feature3" }
            };

            if (!context.Features.Any())
            {
                context.Features.AddRange(features);
                context.SaveChanges();
            }
        }
    }
}