using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Application.Interfaces;

namespace Supply.Api.Controllers
{
    public class VeiculosController : ApiController
    {
        private readonly IVeiculoAppService _veiculoAppService;

        public VeiculosController(IVeiculoAppService veiculoAppService)
        {
            _veiculoAppService = veiculoAppService;
        }

        [HttpGet]
        public IEnumerable<VeiculoDTO> Get()
        {
            return _veiculoAppService.GetAll();
        }

        [HttpGet("{id:guid}")]
        public VeiculoDTO Get(Guid id)
        {
            return _veiculoAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddVeiculoDTO addVeiculoDTO)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _veiculoAppService.Add(addVeiculoDTO));
        }
    }
}
