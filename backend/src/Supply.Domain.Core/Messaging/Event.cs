using System;
using MediatR;

namespace Supply.Domain.Core.Messaging
{
    public abstract class Event : Message, INotification
    {
        protected Event(Guid aggregateId) : base(aggregateId) { }
    }
}
