using FPECS.DSP.SPW.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FPECS.DSP.SPW.Business;

public static class LayerRegistrationExtension
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: services DI configuration

        services.UseDataAccessLayer(configuration);

        return services;
    }
}