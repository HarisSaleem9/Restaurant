using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.User;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorizarion.Requirements.AuthorizeUserWithTwoRestaurants;

public class AuthorizeUserWithTwoRestaurantsRequirementHandler : AuthorizationHandler<AuthorizeUserWithTwoRestaurantsRequirement>
{
    private readonly IUserContext _userContext;
    private readonly IRestaurantRepository _restaurants;

    public AuthorizeUserWithTwoRestaurantsRequirementHandler(IUserContext userContext,IRestaurantRepository restaurants)
    {
        _userContext = userContext;
        _restaurants = restaurants;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeUserWithTwoRestaurantsRequirement requirement)
    {
        var user = _userContext.GetCurrentUser();
        var reststaurants = await _restaurants.GetAllAsync();
        var nofRestaurants = reststaurants.Count(r => r.OwnerId == user.Id);
        if (nofRestaurants >= requirement.OwnerofNumberofRestaurant)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
