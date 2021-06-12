using System;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Events.VehicleEvents
{
    public class VehicleAddedEvent : Event
    {
        public VehicleAddedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
