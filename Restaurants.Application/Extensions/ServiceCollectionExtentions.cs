using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using System.Reflection;
using Restaurants.Application.User;

namespace Restaurants.Application.Extensions;


public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssebly = typeof(ServiceCollectionExtension).Assembly;
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(applicationAssebly);
        services.AddValidatorsFromAssembly(applicationAssebly)
            .AddFluentValidationAutoValidation();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        

    }
}