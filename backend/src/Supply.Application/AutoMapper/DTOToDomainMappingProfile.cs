using System;
using AutoMapper;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Domain.Commands.VeiculoCommands;

namespace Supply.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<AddVeiculoDTO, AddVeiculoCommand>()
                .ConstructUsing(c => new AddVeiculoCommand(c.Placa));
        }
    }
}
