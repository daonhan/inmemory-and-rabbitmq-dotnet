using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supply.Caching.Entities;

namespace Supply.Caching.Interfaces
{
    public interface IVeiculoCacheRepository
    {
        IEnumerable<VeiculoCache> GetAll();
        VeiculoCache GetById(Guid id);

        void Add(VeiculoCache veiculoCache);
    }
}
