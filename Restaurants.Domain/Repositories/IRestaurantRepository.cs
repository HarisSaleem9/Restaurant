using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<Restaurant> GetByIdAsync(int id);
    Task<int> CreateAsync(Restaurant restaurant);
    Task DeleteAsync(Restaurant entity);
    Task<(IEnumerable<Restaurant>,int)> GetAllMachingAsync(string? searchPhrase,int pageSize,int PageNumber,string? sortBy, SortDirection sortDirection);
    Task SaveChanges();
}
