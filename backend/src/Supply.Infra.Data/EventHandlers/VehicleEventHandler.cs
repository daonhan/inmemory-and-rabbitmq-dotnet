using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Events.VehicleEvents;
using Supply.Domain.Interfaces;

namespace Supply.Infra.Data.EventHandlers
{
    public class VehicleEventHandler : 
        INotificationHandler<VehicleAddedEvent>,
        INotificationHandler<VehicleUpdatedEvent>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleCacheRepository _vehicleCacheRepository;

        public VehicleEventHandler(IVehicleRepository vehicleRepository, 
                                   IVehicleCacheRepository vehicleCacheRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleCacheRepository = vehicleCacheRepository;
        }

        public async Task Handle(VehicleAddedEvent notification, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetById(notification.AggregateId);
            var vehicleCache = new VehicleCache(vehicle.Id, vehicle.Plate);

            _vehicleCacheRepository.Add(vehicleCache);
        }

        public async Task Handle(VehicleUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetById(notification.AggregateId);
            var vehicleCache = new VehicleCache(vehicle.Id, vehicle.Plate);

            _vehicleCacheRepository.Update(vehicleCache);
        }
    }
}
