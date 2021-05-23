using AutoMapper;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Caching.Entities;

namespace Supply.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<VeiculoCache, VeiculoDTO>();
        }
    }
}
