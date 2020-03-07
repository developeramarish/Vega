using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vega.Persistence;

namespace Vega.Seeders
{
    public static class Seeder
    {
        public static void Run(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<VegaDbContext>();
                context.Database.Migrate();
                
                MakeSeeder.SeedMakes(context);
                ModelSeeder.SeedModels(context);
            }
        }
    }
}