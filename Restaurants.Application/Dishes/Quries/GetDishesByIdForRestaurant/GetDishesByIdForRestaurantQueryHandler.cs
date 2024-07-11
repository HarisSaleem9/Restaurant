using AutoMapper;
using MediatR;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Quries.GetDishesByIdForRestaurant;

public class GetDishesByIdForRestaurantQueryHandler : IRequestHandler<GetDishesByIdForRestaurantQuery, DishDto>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetDishesByIdForRestaurantQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }
    public async Task<DishDto> Handle(GetDishesByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        var dish = restaurant.Dishes.FirstOrDefault(x=>x.Id == request.DishId);
        var result = _mapper.Map<DishDto>(dish);
        return result;
    }
}
