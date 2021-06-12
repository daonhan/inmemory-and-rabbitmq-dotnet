using FluentValidation.Results;
using MediatR;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Entities;
using Supply.Domain.Events.VehicleEvents;
using Supply.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Domain.CommandHandlers
{
    public class VehicleCommandHandler : CommandHandler,
        IRequestHandler<AddVehicleCommand, ValidationResult>
    {
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleCommandHandler(IMessageBrokerBus messageBrokerBus, 
                                     IVehicleRepository vehicleRepository)
        {
            _messageBrokerBus = messageBrokerBus;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ValidationResult> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var vehicle = new Vehicle(request.Plate);

            if (await _vehicleRepository.GetByPlate(vehicle.Plate) != null)
            {
                AddError(DomainMessages.AlreadyInUse.Format("Plate").Message);
                return ValidationResult;
            }

            _vehicleRepository.Add(vehicle);

            if (await Commit(_vehicleRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VehicleAddedEvent(vehicle.Id));
            }

            return ValidationResult;
        }
    }
}
