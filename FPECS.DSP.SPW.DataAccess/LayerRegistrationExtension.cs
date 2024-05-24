using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FPECS.DSP.SPW.DataAccess;

public static class LayerRegistrationExtension
{
    public static IServiceCollection UseDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: database context configuration

        return services;
    }
}