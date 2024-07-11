using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Quries.GetDishesByIdForRestaurant;

public class GetDishesByIdForRestaurantQuery : IRequest<DishDto>
{
    public int RestaurantId { get; }
    public int DishId { get; }
    public GetDishesByIdForRestaurantQuery(int restaurantId, int dishId)
    {
        RestaurantId = restaurantId;
        DishId = dishId;
    }
    
}
