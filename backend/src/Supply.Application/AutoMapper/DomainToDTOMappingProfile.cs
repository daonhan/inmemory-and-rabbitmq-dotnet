using AutoMapper;
using Supply.Application.DTOs.VehicleDTOs;
using Supply.Caching.Entities;

namespace Supply.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<VehicleCache, VehicleDTO>();
        }
    }
}
