using MediatR;
using Restaurants.Application.Commmon;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Quries.GetAllRestaurant;

public class GetAllRestaurantQuire : IRequest<PageResults<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }   
    public int  PageSize { get; set; }
    public int  PageNumber { get; set;}
    public string? SortBy { get; set; }
    public SortDirection sortDirection { get; set; }
}
