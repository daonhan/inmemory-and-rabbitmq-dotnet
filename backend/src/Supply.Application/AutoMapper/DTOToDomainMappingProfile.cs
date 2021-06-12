using System;
using AutoMapper;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Domain.Commands.VehicleCommands;

namespace Supply.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<AddVehicleDTO, AddVehicleCommand>()
                .ConstructUsing(c => new AddVehicleCommand(c.Plate));
        }
    }
}
