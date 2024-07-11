using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Quries.GetRestaurantById;

public class GetRestaurantByIdQuryHandler : IRequestHandler<GetRestaurantByIdQury, RestaurantDto>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    private readonly IBlobStorageService blobStorageService;

    public GetRestaurantByIdQuryHandler(IRestaurantRepository restaurantRepository, IMapper mapper,IBlobStorageService blobStorageService)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        this.blobStorageService = blobStorageService;
    }
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQury request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
        restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);
        return restaurantDto;
    }
}
