using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VehicleValidators;
using System;

namespace Supply.Domain.Commands.VehicleCommands
{
    public class UpdateVehicleCommand : Command
    {
        public string Plate { get; }

        public UpdateVehicleCommand(Guid id, string plate) : base(id)
        {
            Plate = plate;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateVehicleCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
