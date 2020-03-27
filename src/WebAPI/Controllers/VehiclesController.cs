using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vega.Controllers.Resources;
using Vega.Core;
using Vega.Core.Models;

namespace Vega.Controllers
{
    [Authorize]
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IModelRepository modelRepository;

        public VehiclesController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVehicleRepository vehicleRepository,
            IModelRepository modelRepository
        )
        {
            this.vehicleRepository = vehicleRepository;
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<VehicleResource>> GetVehicles([FromQuery] VehicleQueryResource FilterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(FilterResource);

            var vehicles = await vehicleRepository.GetVehicles(filter);

            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
            // throw new Exception("ooppss!!");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await modelRepository.GetModel(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError(nameof(vehicleResource.ModelId), "Invalid Model.");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            vehicleRepository.Add(vehicle);
            await unitOfWork.Complete();

            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await modelRepository.GetModel(vehicleResource.ModelId);
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

            await unitOfWork.Complete();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound("Vehicle not found.");

            vehicleRepository.Remove(vehicle);
            await unitOfWork.Complete();

            return Ok(id);
        }

    }
}