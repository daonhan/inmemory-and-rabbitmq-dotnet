using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;

namespace Supply.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetById(Guid id);
        Task<Vehicle> GetByPlate(string plate);

        void Add(Vehicle vehicle);
    }
}
