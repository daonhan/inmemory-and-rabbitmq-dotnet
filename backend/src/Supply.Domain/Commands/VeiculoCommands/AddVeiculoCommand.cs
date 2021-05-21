using System;
using FluentValidation.Results;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoValidators;

namespace Supply.Domain.Commands.VeiculoCommands
{
    public class AddVeiculoCommand : Command
    {
        public string Placa { get; }

        public AddVeiculoCommand(string placa) : base(Guid.Empty)
        {
            Placa = placa;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
