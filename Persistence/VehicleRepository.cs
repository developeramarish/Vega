using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Core;
using Vega.Core.Models;

namespace Vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);
                
            return await context.Vehicles
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .Include(v => v.VehicleFeatures)
            .ThenInclude(vf => vf.Feature)
            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async void Add(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await context.Vehicles
            .Include(v => v.VehicleFeatures)
            .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .ToListAsync();
        }
    }
}