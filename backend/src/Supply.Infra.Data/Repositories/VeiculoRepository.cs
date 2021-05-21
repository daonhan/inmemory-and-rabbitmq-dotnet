using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly SupplyContext _supplyContext;

        public VeiculoRepository(SupplyContext supplyContext)
        {
            _supplyContext = supplyContext;
        }

        public IUnitOfWork UnitOfWork => _supplyContext;

        public async Task<IEnumerable<Veiculo>> GetAll()
        {
            return await _supplyContext.Veiculos.ToListAsync();
        }

        public async Task<Veiculo> GetById(Guid id)
        {
            return await _supplyContext.Veiculos.FindAsync(id);
        }

        public async Task<Veiculo> GetByPlaca(string placa)
        {
            return await _supplyContext.Veiculos.AsNoTracking().FirstOrDefaultAsync(x => x.Placa == placa);
        }

        public void Add(Veiculo veiculo)
        {
            _supplyContext.Veiculos.Add(veiculo);
        }
    }
}
