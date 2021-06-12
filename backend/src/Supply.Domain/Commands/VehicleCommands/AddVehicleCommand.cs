using System;
using FluentValidation.Results;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VehicleValidators;

namespace Supply.Domain.Commands.VehicleCommands
{
    public class AddVehicleCommand : Command
    {
        public string Plate { get; }

        public AddVehicleCommand(string plate) : base(Guid.Empty)
        {
            Plate = plate;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVehicleCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
