using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repository;

public class DishRepository : IDishRepository
{
    private readonly RestaurantsDbContext _restaurantsDbContext;

    public DishRepository(RestaurantsDbContext restaurantsDbContext)
    {
        _restaurantsDbContext = restaurantsDbContext;
    }
    public async Task<int> CreateAsync(Dish dish)
    {
        _restaurantsDbContext.Dishes.Add(dish);
        await _restaurantsDbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task DeleteAsync(IEnumerable<Dish> dishes)
    {
        _restaurantsDbContext.Dishes.RemoveRange(dishes);
        await _restaurantsDbContext.SaveChangesAsync();
    }
}
