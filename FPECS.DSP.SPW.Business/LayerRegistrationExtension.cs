using FluentValidation;
using FPECS.DSP.SPW.Business.Services;
using FPECS.DSP.SPW.Business.Validators;
using FPECS.DSP.SPW.Business.Validators.Publications;
using FPECS.DSP.SPW.DataAccess;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Globalization;

namespace FPECS.DSP.SPW.Business;

public static class LayerRegistrationExtension
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        ValidatorOptions.Global.LanguageManager.Enabled = true;
        //ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("uk");
        services.AddValidatorsFromAssemblyContaining<PublicationCreateRequestValidator>();
        services.AddFluentValidationAutoValidation(options =>
        {
            options.OverrideDefaultResultFactoryWith<ValidationResultFactory>();

            options.EnableFormBindingSourceAutomaticValidation = true;
            options.EnablePathBindingSourceAutomaticValidation = true;
            options.EnableQueryBindingSourceAutomaticValidation = true;
            options.EnableBodyBindingSourceAutomaticValidation = true;
        });

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