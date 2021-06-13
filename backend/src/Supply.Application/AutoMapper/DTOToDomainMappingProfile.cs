using AutoMapper;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Domain.Commands.VehicleCommands;
using System;

namespace Supply.Application.AutoMapper
{
    public class DTOToDomainMappingProfile : Profile
    {
        public DTOToDomainMappingProfile()
        {
            CreateMap<AddVehicleDTO, AddVehicleCommand>()
                .ConstructUsing(c => new AddVehicleCommand(c.Plate));

            CreateMap<UpdateVehicleDTO, UpdateVehicleCommand>()
                .ConstructUsing(c => new UpdateVehicleCommand(c.Id, c.Plate));

            CreateMap<Guid, RemoveVehicleCommand>()
                .ConstructUsing(c => new RemoveVehicleCommand(c));
        }
    }
}
