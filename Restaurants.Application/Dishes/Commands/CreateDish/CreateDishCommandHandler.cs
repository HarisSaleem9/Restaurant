using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;
public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand,Dish>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IDishRepository _dishRepository;

    public CreateDishCommandHandler(IRestaurantRepository restaurantRepository,IMapper mapper,
        IDishRepository dishRepository)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }
    public async Task<Dish> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        var dish = _mapper.Map<Dish>(request);
        await _dishRepository.CreateAsync(dish);
        return dish;

    }
}
