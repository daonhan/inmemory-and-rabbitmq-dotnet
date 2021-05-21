using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Supply.Application.DTOs.VeiculoDTOs;

namespace Supply.Application.Interfaces
{
    public interface IVeiculoAppService
    {
        Task<IEnumerable<VeiculoDTO>> GetAll();
        Task<VeiculoDTO> GetById(Guid id);

        Task<ValidationResult> Add(AddVeiculoDTO addVeiculoDTO);
    }
}
