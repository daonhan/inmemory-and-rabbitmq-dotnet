using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Supply.Receiver.Configurations;
using System;
using System.Reflection;

namespace Supply.Receiver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.Development.json", true, true)
                    .Build();

                CreateHostBuilder(args, config).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    // Settings Receiver
                    services.AddHostedService<Receiver>();

                    // Setting DBContexts
                    services.AddDatabaseConfiguration(configuration);

                    // Setting RabbitMq
                    services.AddRabbitMqConfiguration(configuration);

                    // Setting AutoMapper
                    services.AddAutoMapperConfiguration();

                    // Setting MediatR
                    services.AddMediatR(Assembly.GetExecutingAssembly());

                    // .NET Native DI Abstraction
                    services.AddDependencyInjectionConfiguration();
                });
    }
}
