using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SupplyDataContext _supplyContext;

        public VehicleRepository(SupplyDataContext supplyContext)
        {
            _supplyContext = supplyContext;
        }

        public IUnitOfWork UnitOfWork => _supplyContext;

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _supplyContext.Vehicles.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> Search(Expression<Func<Vehicle, bool>> predicate)
        {
            return await _supplyContext.Vehicles.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<Vehicle> GetById(Guid id)
        {
            return await _supplyContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            _supplyContext.Vehicles.Add(vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            _supplyContext.Vehicles.Update(vehicle);
        }
    }
}
