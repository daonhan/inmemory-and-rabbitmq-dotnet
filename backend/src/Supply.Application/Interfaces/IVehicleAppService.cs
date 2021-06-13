using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Supply.Application.DTOs.VehicleDTOs;

namespace Supply.Application.Interfaces
{
    public interface IVehicleAppService
    {
        IEnumerable<VehicleDTO> GetAll();
        VehicleDTO GetById(Guid id);

        Task<ValidationResult> Add(AddVehicleDTO addVehicleDTO);
        Task<ValidationResult> Update(UpdateVehicleDTO updateVehicleDTO);
        Task<ValidationResult> Remove(Guid id);
    }
}
