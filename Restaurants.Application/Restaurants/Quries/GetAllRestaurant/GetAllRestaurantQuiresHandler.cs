using AutoMapper;
using MediatR;
using Restaurants.Application.Commmon;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Quries.GetAllRestaurant;

public class GetAllRestaurantQuiresHandler : IRequestHandler<GetAllRestaurantQuire, PageResults<RestaurantDto>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetAllRestaurantQuiresHandler(IRestaurantRepository restaurantRepository , IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }
    public async Task<PageResults<RestaurantDto>> Handle(GetAllRestaurantQuire request, CancellationToken cancellationToken)
    {
        var (restaurants,TotalCount) = await _restaurantRepository.GetAllMachingAsync(request.SearchPhrase,
            request.PageSize, 
            request.PageNumber,
            request.SortBy,
            request.sortDirection);
        var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        var result = new PageResults<RestaurantDto>(restaurantDtos, TotalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
