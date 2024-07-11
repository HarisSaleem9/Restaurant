using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder : IRestaurantSeeder
{
    private readonly RestaurantsDbContext dbContext;

    public RestaurantSeeder(RestaurantsDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole(UserRoles.User)
            {
                NormalizedName = UserRoles.User.ToUpper()
            },
            new IdentityRole(UserRoles.Owner)
            {
                NormalizedName = UserRoles.Owner.ToUpper()
            },
            new IdentityRole(UserRoles.Admin) { NormalizedName = UserRoles.Admin.ToUpper() }
        };
        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        User owner = new User()
        {
            Email = "seed-user@test.com"
        };
        List<Restaurant> restaurants = new List<Restaurant> {
             new()
             {
                 Owner = owner,
                 Name = "fc",
                 Category = "FastFood",
                 Description = "sdadssdad aasdasdsa dsad as",
                 ContactEmail = "contact@mail.com",
                 HasDelivery = true,
                 Dishes = new List<Dish>{
                 new()
                 {
                     Name = "Hot chicken",
                     Description = "This is Hot Chicken",
                     Price = 10
                 }
                 },
                 Address =
                 new()
                 {
                     City = "pakistan",
                     PostalCode = "44000",
                     Street = "32 A4"
                 }

             }
             };
        return restaurants;
    }
}
