using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Application.Interfaces;
using Supply.Caching.Interfaces;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Interfaces;

namespace Supply.Application.Services
{
    public class VeiculoAppService : IVeiculoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVeiculoCacheRepository _veiculoCacheRepository;

        public VeiculoAppService(IMapper mapper,
                                 IMediatorHandler mediator,
                                 IVeiculoRepository veiculoRepository, 
                                 IVeiculoCacheRepository veiculoCacheRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _veiculoRepository = veiculoRepository;
            _veiculoCacheRepository = veiculoCacheRepository;
        }

        public IEnumerable<VeiculoDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<VeiculoDTO>>(_veiculoCacheRepository.GetAll());
        }

        public VeiculoDTO GetById(Guid id)
        {
            return _mapper.Map<VeiculoDTO>(_veiculoCacheRepository.GetById(id));
        }

        public async Task<ValidationResult> Add(AddVeiculoDTO addVeiculoDTO)
        {
            return await _mediator.SendCommand(_mapper.Map<AddVeiculoCommand>(addVeiculoDTO));
        }
    }
}
