using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistence;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly VegaDbContext context;
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, VegaDbContext context)
        {
            this.vehicleRepository = vehicleRepository;
            this.context = context;
            this.mapper = mapper;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle([FromRoute]int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound("Vehicle not found.");

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError(nameof(vehicleResource.ModelId), "Invalid Model.");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            await context.Vehicles.AddAsync(vehicle);
            await context.SaveChangesAsync();

            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await context.Models.FindAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError(nameof(vehicleResource.ModelId), "Invalid Model.");
                return BadRequest(ModelState);
            }

            var vehicle = await vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound("Vehicle not found.");

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound("Vehicle not found.");

            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();

            return Ok(id);
        }

    }
}