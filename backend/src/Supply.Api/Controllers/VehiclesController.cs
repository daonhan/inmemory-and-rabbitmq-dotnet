using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Application.Interfaces;

namespace Supply.Api.Controllers
{
    public class VehiclesController : ApiController
    {
        private readonly IVehicleAppService _vehicleAppService;

        public VehiclesController(IVehicleAppService vehicleAppService)
        {
            _vehicleAppService = vehicleAppService;
        }

        [HttpGet]
        public IEnumerable<VehicleDTO> Get()
        {
            return _vehicleAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public VehicleDTO Get(Guid id)
        {
            return _vehicleAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddVehicleDTO addVehicleDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _vehicleAppService.Add(addVehicleDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleDTO updateVehicleDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _vehicleAppService.Update(updateVehicleDTO));
        }
    }
}
