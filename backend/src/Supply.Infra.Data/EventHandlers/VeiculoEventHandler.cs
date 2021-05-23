using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Domain.Interfaces;

namespace Supply.Infra.Data.EventHandlers
{
    public class VeiculoEventHandler : INotificationHandler<VeiculoAddedEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVeiculoCacheRepository _veiculoCacheRepository;

        public VeiculoEventHandler(IVeiculoRepository veiculoRepository, 
                                   IVeiculoCacheRepository veiculoCacheRepository)
        {
            _veiculoRepository = veiculoRepository;
            _veiculoCacheRepository = veiculoCacheRepository;
        }

        public async Task Handle(VeiculoAddedEvent notification, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepository.GetById(notification.AggregateId);
            var veiculoCache = new VeiculoCache(veiculo.Id, veiculo.Placa);

            _veiculoCacheRepository.Add(veiculoCache);
        }
    }
}
