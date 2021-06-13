using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories
{
    public class VehicleCacheRepository : IVehicleCacheRepository
    {
        private readonly SupplyCacheContext _supplyCacheContext;

        public VehicleCacheRepository(SupplyCacheContext supplyCacheContext)
        {
            _supplyCacheContext = supplyCacheContext;
        }

        public IEnumerable<VehicleCache> GetAll()
        {
            return _supplyCacheContext.VehiclesCache.Find(_ => true).ToList();
        }

        public VehicleCache GetById(Guid id)
        {
            return _supplyCacheContext.VehiclesCache.Find(e => e.Id == id.ToString()).SingleOrDefault();
        }

        public void Add(VehicleCache vehicleCache)
        {
            _supplyCacheContext.VehiclesCache.InsertOne(vehicleCache);
        }

        public void Update(VehicleCache vehicleCache)
        {
            _supplyCacheContext.VehiclesCache.ReplaceOne(x => x.Id == vehicleCache.Id, vehicleCache);
        }

        public void Remove(Guid id)
        {
            _supplyCacheContext.VehiclesCache.DeleteOne(x => x.Id == id.ToString());
        }
    }
}
