using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Supply.Domain.Interfaces
{
    public interface IVehicleRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<Vehicle>> GetAll();
        Task<IEnumerable<Vehicle>> Search(Expression<Func<Vehicle, bool>> predicate);
        Task<Vehicle> GetById(Guid id);

        void Add(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
