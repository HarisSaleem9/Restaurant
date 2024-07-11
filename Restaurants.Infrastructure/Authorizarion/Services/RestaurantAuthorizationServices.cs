using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interface;

namespace Restaurants.Infrastructure.Authorizarion.Services;

public class RestaurantAuthorizationServices : IRestaurantAuthorizationServices
{
    private readonly IUserContext _userContext;

    public RestaurantAuthorizationServices(IUserContext userContext)
    {
        _userContext = userContext;
    }
    public bool Authorize(Restaurant restaurants, ResourceOperation resourceOperation)
    {
        var user = _userContext.GetCurrentUser();
        if (resourceOperation == ResourceOperation.Read)
        {
            return true;
        }
        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            return true;
        }
        if ((resourceOperation == ResourceOperation.Create || resourceOperation == ResourceOperation.Update)
            && user.IsInRole(UserRoles.Admin))
        {
            return true;
        }
        return false;

    }
}
