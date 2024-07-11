using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using Restaurants.Application.Extensions;
using Restaurants.Infrastructure.Extensions;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

namespace Restaurant.Api.Extentions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme,Id="bearerAuth"}
            },
            new List<string>()
        }
    });
        });
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddEndpointsApiExplorer();
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
            .WriteTo.Console();
        });

    } 
}
