using System;
using FluentValidation.Results;
using MediatR;

namespace Supply.Domain.Core.Messaging
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public ValidationResult ValidationResult { get; protected set; }

        protected Command(Guid aggregateId) : base(aggregateId) { }

        public abstract bool IsValid();
    }
}
