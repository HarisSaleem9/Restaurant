using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommand : IRequest<string>
{
    public int RestaurantId { get; init; }
}
