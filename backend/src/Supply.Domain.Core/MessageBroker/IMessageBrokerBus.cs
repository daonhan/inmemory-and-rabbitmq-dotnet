using Supply.Domain.Core.Messaging;
using System.Threading.Tasks;

namespace Supply.Domain.Core.MessageBroker
{
    public interface IMessageBrokerBus
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}
