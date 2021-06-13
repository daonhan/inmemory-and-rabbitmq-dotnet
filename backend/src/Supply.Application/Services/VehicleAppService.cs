using AutoMapper;
using FluentValidation.Results;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supply.Application.Services
{
    public class VehicleAppService : IVehicleAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleCacheRepository _vehicleCacheRepository;

        public VehicleAppService(IMapper mapper,
                                 IMediatorHandler mediator,
                                 IVehicleRepository vehicleRepository, 
                                 IVehicleCacheRepository vehicleCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _vehicleRepository = vehicleRepository;
            _vehicleCacheRepository = vehicleCacheRepository;
        }

        public IEnumerable<VehicleDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<VehicleDTO>>(_vehicleCacheRepository.GetAll());
        }

        public VehicleDTO GetById(Guid id)
        {
            return _mapper.Map<VehicleDTO>(_vehicleCacheRepository.GetById(id));
        }

        public async Task<ValidationResult> Add(AddVehicleDTO addVehicleDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVehicleCommand>(addVehicleDTO));
        }

        public async Task<ValidationResult> Update(UpdateVehicleDTO updateVehicleDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<UpdateVehicleCommand>(updateVehicleDTO));
        }
    }
}
