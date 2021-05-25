using Microsoft.Extensions.DependencyInjection;
using Supply.Application.AutoMapper;
using System;

namespace Supply.Receiver.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile), typeof(DTOToDomainMappingProfile));
        }
    }
}
