using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoCommandHandler : IRequestHandler<UploadRestaurantLogoCommand,string>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRestaurantAuthorizationServices _restaurantAuthorizationServices;
    private readonly IBlobStorageService _blobStorageService;

    public UploadRestaurantLogoCommandHandler(IRestaurantRepository restaurantRepository,IRestaurantAuthorizationServices restaurantAuthorizationServices,IBlobStorageService blobStorageService)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantAuthorizationServices = restaurantAuthorizationServices;
        _blobStorageService = blobStorageService;
    }
    public async Task<string> Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        var restaurant =  await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (!_restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Create))
        {
            throw new Exception("Can't Create Restaurant Because you are not Admin");
        }
        var logoUrl = await _blobStorageService.UploadToBlobAsync(request.File, request.FileName);
        restaurant.LogoUrl = logoUrl;

        await _restaurantRepository.SaveChanges();
        return $"Upload Successfull with Url # {restaurant.LogoUrl}";
    }
}
