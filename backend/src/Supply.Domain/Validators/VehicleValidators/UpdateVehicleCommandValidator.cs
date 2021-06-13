using FluentValidation;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VehicleValidators
{
    public class UpdateVehicleCommandValidator : CommandValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);

            RuleFor(x => x.Plate)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Plate").Message);

            RuleFor(x => x.Plate)
                .Matches("^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$")
                .WithMessage(DomainMessages.InvalidFormat.Format("Plate").Message)
                .When(x => !string.IsNullOrEmpty(x.Plate));
        }
    }
}
