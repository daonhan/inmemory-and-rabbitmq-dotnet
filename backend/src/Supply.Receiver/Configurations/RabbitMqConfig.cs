using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supply.Domain.Core.MessageBroker.Options;
using System;

namespace Supply.Receiver.Configurations
{
    public static class RabbitMqConfig
    {
        public static void AddRabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMqOptions"));
        }
    }
}
