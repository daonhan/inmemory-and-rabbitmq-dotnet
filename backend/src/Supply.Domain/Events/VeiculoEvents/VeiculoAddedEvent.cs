using System;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Events.VeiculoEvents
{
    public class VeiculoAddedEvent : Event
    {
        public VeiculoAddedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
