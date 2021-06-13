using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supply.Caching.Entities;

namespace Supply.Caching.Interfaces
{
    public interface IVehicleCacheRepository
    {
        IEnumerable<VehicleCache> GetAll();
        VehicleCache GetById(Guid id);

        void Add(VehicleCache vehicleCache);
        void Update(VehicleCache vehicleCache);
        void Remove(Guid id);
    }
}
