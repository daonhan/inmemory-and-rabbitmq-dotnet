using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Supply.Application.Interfaces;
using Supply.Application.Services;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Mediator;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;
using Supply.Infra.Data.Repositories;

namespace Supply.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IVeiculoAppService, VeiculoAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AddVeiculoCommand, ValidationResult>, VeiculoCommandHandler>();

            // Infra - Data
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<SupplyContext>();
        }
    }
}
