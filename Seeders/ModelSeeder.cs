using System.Collections.Generic;
using System.Linq;
using Vega.Core.Models;
using Vega.Persistence;

namespace Vega.Seeders
{
    public static class ModelSeeder
    {
        public static void SeedModels(VegaDbContext context)
        {
            var make1Id = context.Makes.SingleOrDefault(make => make.Name == "Make1").Id;
            var make2Id = context.Makes.SingleOrDefault(make => make.Name == "Make2").Id;

            var models = new List<Model>
            {
                new Model { Name = "Model 1 - Make 1", MakeId = make1Id },
                new Model { Name = "Model 2 - Make 1", MakeId = make1Id },
                new Model { Name = "Model 1 - Make 2", MakeId = make2Id },
                new Model { Name = "Model 2 - Make 2", MakeId = make2Id },
            };

            if (!context.Models.Any())
            {
                context.Models.AddRange(models);
                context.SaveChanges();
            }

        }

    }
}