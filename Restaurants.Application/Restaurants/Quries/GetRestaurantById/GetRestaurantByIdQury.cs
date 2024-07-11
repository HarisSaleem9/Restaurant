using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Quries.GetRestaurantById;

public class GetRestaurantByIdQury : IRequest<RestaurantDto>
{
    public int Id { get; init; }
}
