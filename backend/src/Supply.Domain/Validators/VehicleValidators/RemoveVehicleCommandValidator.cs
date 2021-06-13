using FluentValidation;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VehicleValidators
{
    public class RemoveVehicleCommandValidator : CommandValidator<RemoveVehicleCommand>
    {
        public RemoveVehicleCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
