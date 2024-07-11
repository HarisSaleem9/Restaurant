using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorizarion;
using Restaurants.Infrastructure.Authorizarion.Requirements;
using Restaurants.Infrastructure.Authorizarion.Requirements.AuthorizeUserWithTwoRestaurants;
using Restaurants.Infrastructure.Authorizarion.Services;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repository;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration confguration)
    {
        var connectionString = confguration.GetConnectionString("RestaurantsDb");
        services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddTransient<IDishRepository,DishRepository>();
        services.AddIdentity<User,IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipleFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();
        services.AddAuthorization(options =>
        {
        options.AddPolicy(PolicyName.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality));
        options.AddPolicy(PolicyName.AtLeast20, builder => builder.AddRequirements(new MinimumAgeRequirements(20)));
            options.AddPolicy(PolicyName.AtLeast2Restaurants,builder => builder.AddRequirements(new AuthorizeUserWithTwoRestaurantsRequirement(2))); 
        });
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementsHandler>();
        services.AddScoped<IAuthorizationHandler, AuthorizeUserWithTwoRestaurantsRequirementHandler>();
        services.AddScoped<IRestaurantAuthorizationServices, RestaurantAuthorizationServices>();
        services.Configure<BlobStorageSettings>(confguration.GetSection("BlobStorage"));
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
