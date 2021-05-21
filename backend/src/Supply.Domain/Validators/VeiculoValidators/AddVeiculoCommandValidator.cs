using FluentValidation;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoValidators
{
    class AddVeiculoCommandValidator : CommandValidator<AddVeiculoCommand>
    {
        public AddVeiculoCommandValidator()
        {
            RuleFor(x => x.Placa)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Placa").Message);

            RuleFor(x => x.Placa)
                .Matches("^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$")
                .WithMessage(DomainMessages.InvalidFormat.Format("Placa").Message)
                .When(x => !string.IsNullOrEmpty(x.Placa));
        }
    }
}
