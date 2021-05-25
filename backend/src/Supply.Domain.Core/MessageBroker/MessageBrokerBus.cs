using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Supply.Domain.Core.MessageBroker.Options;
using Supply.Domain.Core.Messaging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Supply.Domain.Core.MessageBroker
{
    public class MessageBrokerBus : IMessageBrokerBus
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;

        public MessageBrokerBus(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _hostName = rabbitMqOptions.Value.HostName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't create connection with RabbitMq: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }

        public Task PublishEvent<T>(T @event) where T : Event
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: @event.MessageType, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: @event.MessageType, basicProperties: null, body: body);

                    Console.WriteLine($"Published event to RabbitMq: {@event.MessageType} - {@event.AggregateId}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
