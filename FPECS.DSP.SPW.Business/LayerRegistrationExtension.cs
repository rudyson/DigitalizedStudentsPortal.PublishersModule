using FPECS.DSP.SPW.Business.Services;
using FPECS.DSP.SPW.DataAccess;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FPECS.DSP.SPW.Business;

public static class LayerRegistrationExtension
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IResearcherService, ResearcherService>();
        services.AddScoped<IPublicationService, PublicationService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IDisciplineService, DisciplineService>();
        
        services.AddMapster();
        services.RegisterMapsterConfiguration();
        services.UseDataAccessLayer(configuration);

        return services;
    }
}