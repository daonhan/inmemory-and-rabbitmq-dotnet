using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Core.MessageBroker.Options;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Events.VehicleEvents;
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
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ILogger<Receiver> _logger;

        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;

        private IConnection _connection;
        private IModel _channel;

        public Receiver(IOptions<RabbitMqOptions> rabbitMqOptions,
                        IMediatorHandler mediatorHandler, 
                        ILogger<Receiver> logger)
        {
            _mediatorHandler = mediatorHandler;
            _logger = logger;

            _queueList = new List<QueueInfo>()
            {
                new QueueInfo(typeof(VehicleAddedEvent).Name),
                new QueueInfo(typeof(VehicleUpdatedEvent).Name),
                new QueueInfo(typeof(VehicleRemovedEvent).Name)
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

            if (content.Contains(typeof(VehicleAddedEvent).Name))
            {
                await ProcessEvent<VehicleAddedEvent>(content, @event);
                return;
            }

            if (content.Contains(typeof(VehicleUpdatedEvent).Name))
            {
                await ProcessEvent<VehicleUpdatedEvent>(content, @event);
                return;
            }

            if (content.Contains(typeof(VehicleRemovedEvent).Name))
            {
                await ProcessEvent<VehicleRemovedEvent>(content, @event);
                return;
            }

            throw new Exception($"Queue not found for this message type: {content}");
        }

        private async Task ProcessEvent<T>(string content, BasicDeliverEventArgs @event) where T : Event
        {
            var message = JsonConvert.DeserializeObject<T>(content);

            var eventMessage = (Event)message;

            await _mediatorHandler.PublishEvent(eventMessage);

            _channel.BasicAck(@event.DeliveryTag, false);

            _logger.LogInformation($"Event consumed: {eventMessage.MessageType} - {eventMessage.AggregateId}");
        }
    }
}
