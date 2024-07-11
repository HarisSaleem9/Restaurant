using MediatR;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repository;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantsDbContext _restaurantsDbContext;

    public RestaurantRepository(RestaurantsDbContext restaurantsDbContext)
    {
        _restaurantsDbContext = restaurantsDbContext;
    }

    public async Task<int> CreateAsync(Restaurant restaurant)
    {
        _restaurantsDbContext.Restaurants.Add(restaurant);
        await _restaurantsDbContext.SaveChangesAsync();
        return restaurant.Id; 
    }

    public async Task DeleteAsync(Restaurant entity)
    {
        _restaurantsDbContext.Restaurants.Remove(entity);
        await _restaurantsDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
        var restuarnats = await _restaurantsDbContext.Restaurants
            .Include(r=>r.Dishes)
            .ToListAsync();
        return restuarnats;

        }
    public async Task<(IEnumerable<Restaurant>,int)> GetAllMachingAsync(string? searchPhrase, int pageSize, int PageNumber,string? sortBy,SortDirection sortDirection )
        {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = _restaurantsDbContext.Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower) ||
                                                    r.Description.ToLower().Contains(searchPhraseLower)));
        int TotalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name),r=>r.Name},
                { nameof(Restaurant.Description),r=>r.Description},
                { nameof(Restaurant.Category),r=>r.Category}
            };
            var selectedOption = columnSelector[sortBy];
            baseQuery = sortDirection==SortDirection.Assending
                ? baseQuery.OrderBy(selectedOption)
                : baseQuery.OrderByDescending(selectedOption);
        }
        var restuarnats = await baseQuery
            .Skip(pageSize * (PageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        return (restuarnats,TotalCount);

        }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        var restaurant = await _restaurantsDbContext.Restaurants
            .Include(r=>r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }
    public async Task SaveChanges()
    {
        await _restaurantsDbContext.SaveChangesAsync();
    }
}
