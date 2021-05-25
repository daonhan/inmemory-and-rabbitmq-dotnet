﻿using FluentValidation.Results;
using MediatR;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Entities;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Domain.CommandHandlers
{
    public class VeiculoCommandHandler : CommandHandler,
        IRequestHandler<AddVeiculoCommand, ValidationResult>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoCommandHandler(IMediatorHandler mediatorHandler, 
                                     IMessageBrokerBus messageBrokerBus, 
                                     IVeiculoRepository veiculoRepository)
        {
            _mediatorHandler = mediatorHandler;
            _messageBrokerBus = messageBrokerBus;
            _veiculoRepository = veiculoRepository;
        }

        public async Task<ValidationResult> Handle(AddVeiculoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculo = new Veiculo(request.Placa);

            if (await _veiculoRepository.GetByPlaca(veiculo.Placa) != null)
            {
                AddError(DomainMessages.AlreadyInUse.Format("Placa").Message);
                return ValidationResult;
            }

            _veiculoRepository.Add(veiculo);

            if (await Commit(_veiculoRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoAddedEvent(veiculo.Id));
            }

            return ValidationResult;
        }
    }
}
