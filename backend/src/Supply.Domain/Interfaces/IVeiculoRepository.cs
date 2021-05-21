using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;

namespace Supply.Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<Veiculo>> GetAll();
        Task<Veiculo> GetById(Guid id);
        Task<Veiculo> GetByPlaca(string placa);

        void Add(Veiculo veiculo);
    }
}
