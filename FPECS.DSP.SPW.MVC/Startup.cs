using System.Net.Http.Headers;
using System.Text.Json;
using FPECS.DSP.SPW.Business;
using FPECS.DSP.SPW.MVC.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace FPECS.DSP.SPW.MVC;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var azureActiveDirectoryConfigurationSection = configuration.GetSection("AzureAd");
        var microsoftGraphConfigurationSection = configuration.GetSection("MicrosoftGraph");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(azureActiveDirectoryConfigurationSection);

        services.AddRouting(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        services.AddHttpContextAccessor();

        services.AddBusinessLayer(configuration);

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:4200", "https://localhost:4200","http://localhost:4200/", "https://localhost:4200/");
                });
            options.AddPolicy("AzureADPolicy",
                builder =>
                {
                    builder.WithOrigins("https://login.microsoftonline.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<SecurityHeadersMiddleware>();

        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
