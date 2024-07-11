using AutoMapper;
using MediatR;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    private readonly IRestaurantAuthorizationServices _restaurantAuthorizationServices;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository,IMapper mapper,IUserContext userContext,IRestaurantAuthorizationServices restaurantAuthorizationServices)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _userContext = userContext;
        _restaurantAuthorizationServices = restaurantAuthorizationServices;
    }
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();
        var restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;
        if (!_restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Create))
        {
            throw new Exception("Can't Create Restaurant Because you are not Admin");
        }
        int id = await _restaurantRepository.CreateAsync(restaurant);
        return id;
    }
}
