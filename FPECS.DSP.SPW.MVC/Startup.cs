using System.Text.Json;
using FPECS.DSP.SPW.Business;
using FPECS.DSP.SPW.MVC.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace FPECS.DSP.SPW.MVC;

public class Startup(IConfiguration configuration)
{
    private string? GetAzureAdScopeSection => configuration["MicrosoftGraph:Scopes"] ?? configuration["DownstreamApi:Scopes"];
    public void ConfigureServices(IServiceCollection services)
    {
        var azureActiveDirectoryConfigurationSection = configuration.GetSection("AzureAd");
        var microsoftGraphConfigurationSection = configuration.GetSection("MicrosoftGraph");

        // ASP.NET Core MVC
        /*
        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(azureActiveDirectoryConfigurationSection)
            .EnableTokenAcquisitionToCallDownstreamApi(GetAzureAdScopeSection?.Split(' '))
            .AddMicrosoftGraph(microsoftGraphConfigurationSection)
            .AddInMemoryTokenCaches();*/

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(azureActiveDirectoryConfigurationSection);
            //.EnableTokenAcquisitionToCallDownstreamApi()
            //.AddMicrosoftGraph(microsoftGraphConfigurationSection);
            //.AddInMemoryTokenCaches();

        // ASP.NET Core Web API
        /*
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddMicrosoftIdentityWebApi(azureActiveDirectoryConfigurationSection)
           .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(microsoftGraphConfigurationSection)
            .AddInMemoryTokenCaches();*/
        /*
        var jwtBearerOpenIdConnectAuthorizationPolicyBuilder =
            new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme,
                    OpenIdConnectDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(jwtBearerOpenIdConnectAuthorizationPolicyBuilder);
        */
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
                        .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:4200/", "https://localhost:4200/");
                });
            options.AddPolicy("AzureADPolicy",
                builder =>
                {
                    builder.WithOrigins("https://login.microsoftonline.com/72e42a61-9cee-4b78-8828-29b226163bd7")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services
            .AddControllers()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });
        /*
        services.AddControllersWithViews(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        })
            .AddMicrosoftIdentityUI()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });*/

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
            // app.UseSwaggerUI(c => c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "FPECS.DSP.SPW.Api"));
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<SecurityHeadersMiddleware>();

        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            /*
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");*/
            endpoints.MapControllers();
            //endpoints.MapRazorPages();

            //endpoints.MapIdentityApi<ApplicationUser>();

            //endpoints.MapFallbackToFile("/index.html");
        });
    }
}
