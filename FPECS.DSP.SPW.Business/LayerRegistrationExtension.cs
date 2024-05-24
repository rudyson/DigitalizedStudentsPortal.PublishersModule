using FPECS.DSP.SPW.DataAccess;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FPECS.DSP.SPW.Business;

public static class LayerRegistrationExtension
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: services DI configuration
        services.AddMapster();
        services.RegisterMapsterConfiguration();
        services.UseDataAccessLayer(configuration);

        return services;
    }
}