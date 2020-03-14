using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Persistence
{
    public class VehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        
        public async Task<Vehicle> GetVehicle(int id)
        {
            return await context.Vehicles
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.VehicleFeatures)
            .ThenInclude(vf => vf.Feature)
            .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}