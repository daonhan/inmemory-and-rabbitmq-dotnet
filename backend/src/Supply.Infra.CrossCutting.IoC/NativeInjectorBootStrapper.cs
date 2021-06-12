using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Supply.Application.Interfaces;
using Supply.Application.Services;
using Supply.Caching.Interfaces;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VehicleCommands;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Events.VehicleEvents;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;
using Supply.Infra.Data.EventHandlers;
using Supply.Infra.Data.Repositories;

namespace Supply.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IVehicleAppService, VehicleAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain Bus (MessageBroker)
            services.AddScoped<IMessageBrokerBus, MessageBrokerBus>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AddVehicleCommand, ValidationResult>, VehicleCommandHandler>();

            // Domain - Events
            services.AddScoped<INotificationHandler<VehicleAddedEvent>, VehicleEventHandler>();

            // Infra - Data
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleCacheRepository, VehicleCacheRepository>();
            services.AddScoped<SupplyDataContext>();
            services.AddScoped<SupplyCacheContext>();
        }
    }
}
