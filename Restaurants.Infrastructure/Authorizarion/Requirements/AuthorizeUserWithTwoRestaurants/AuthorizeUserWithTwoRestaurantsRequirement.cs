using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorizarion.Requirements.AuthorizeUserWithTwoRestaurants;

public class AuthorizeUserWithTwoRestaurantsRequirement : IAuthorizationRequirement
{
    public int OwnerofNumberofRestaurant { get; }
    public AuthorizeUserWithTwoRestaurantsRequirement(int OwnerOfNR)
    {
        OwnerofNumberofRestaurant = OwnerOfNR;
    }
}
