using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Core.MessageBroker.Options;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Receiver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Receiver
{
    public class Receiver : BackgroundService
    {
        private readonly IEnumerable<QueueInfo> _queueList;

        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        private IConnection _connection;
        private IModel _channel;

        private readonly IMediatorHandler _mediatorHandler;

        public Receiver(IOptions<RabbitMqOptions> rabbitMqOptions,
                        IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;

            _queueList = new List<QueueInfo>()
            {
                new QueueInfo(typeof(VeiculoAddedEvent).Name)
            };

            _hostName = rabbitMqOptions.Value.HostName;
            _userName = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            foreach (var queueInfo in _queueList)
            {
                _channel.QueueDeclare(queue: queueInfo.Name, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnEventReceived;

            foreach (var queueInfo in _queueList)
            {
                _channel.BasicConsume(queueInfo.Name, false, consumer);
            }

            return Task.CompletedTask;
        }

        private async Task OnEventReceived(object sender, BasicDeliverEventArgs @event)
        {
            var content = Encoding.UTF8.GetString(@event.Body.ToArray());

            if (content.Contains(typeof(VeiculoAddedEvent).Name))
            {
                await ProcessEvent<VeiculoAddedEvent>(content, @event);
                return;
            }

            throw new Exception($"Sem Queue encontrada para a mensagem: {content}");
        }

        private async Task ProcessEvent<T>(string content, BasicDeliverEventArgs @event) where T : Event
        {
            var message = JsonConvert.DeserializeObject<T>(content);

            var eventMessage = (Event)message;

            await _mediatorHandler.PublishEvent(eventMessage);

            _channel.BasicAck(@event.DeliveryTag, false);

            Console.WriteLine($"Consumed event: {eventMessage.MessageType} - {eventMessage.AggregateId}");
        }
    }
}
