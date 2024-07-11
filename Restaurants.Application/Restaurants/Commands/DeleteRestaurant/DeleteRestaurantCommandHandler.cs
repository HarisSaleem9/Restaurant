using AutoMapper;
using MediatR;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand,bool>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IRestaurantAuthorizationServices _restaurantAuthorizationServices;

    public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMapper mapper,IRestaurantAuthorizationServices restaurantAuthorizationServices)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _restaurantAuthorizationServices = restaurantAuthorizationServices;
    }
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            return false;
        }
        if (!_restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Delete))
        {
            throw new Exception("Can't delete Restaurant Because you are not Admin"); 
        }
        await _restaurantRepository.DeleteAsync(restaurant);
        return true;    
    }
}
