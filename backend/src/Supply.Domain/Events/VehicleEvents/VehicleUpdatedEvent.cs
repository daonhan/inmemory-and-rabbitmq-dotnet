using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VehicleEvents
{
    public class VehicleUpdatedEvent : Event
    {
        public VehicleUpdatedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
