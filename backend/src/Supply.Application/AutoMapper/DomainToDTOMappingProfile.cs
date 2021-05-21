using AutoMapper;
using Supply.Application.DTOs.VeiculoDTOs;
using Supply.Domain.Entities;

namespace Supply.Application.AutoMapper
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Veiculo, VeiculoDTO>();
        }
    }
}
