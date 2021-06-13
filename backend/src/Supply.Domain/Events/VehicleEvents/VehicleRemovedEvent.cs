using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VehicleEvents
{
    public class VehicleRemovedEvent : Event
    {
        public VehicleRemovedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
