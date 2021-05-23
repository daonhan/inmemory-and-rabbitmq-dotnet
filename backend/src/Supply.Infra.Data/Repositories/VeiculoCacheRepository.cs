using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories
{
    public class VeiculoCacheRepository : IVeiculoCacheRepository
    {
        private readonly SupplyCacheContext _supplyCacheContext;

        public VeiculoCacheRepository(SupplyCacheContext supplyCacheContext)
        {
            _supplyCacheContext = supplyCacheContext;
        }

        public IEnumerable<VeiculoCache> GetAll()
        {
            return _supplyCacheContext.VeiculosCache.Find(_ => true).ToList();
        }

        public VeiculoCache GetById(Guid id)
        {
            return _supplyCacheContext.VeiculosCache.Find(e => e.Id == id).SingleOrDefault();
        }

        public void Add(VeiculoCache veiculoCache)
        {
            _supplyCacheContext.VeiculosCache.InsertOne(veiculoCache);
        }
    }
}
