using AutoMapper;
using MediatR;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand,string>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public DeleteDishesForRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMapper mapper,
        IDishRepository dishRepository)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }
    public async Task<string> Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        await _dishRepository.DeleteAsync(restaurant.Dishes);
        return "Deleted Successfull";
    }
}
