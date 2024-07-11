using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorizarion.Requirements;

public class MinimumAgeRequirements : IAuthorizationRequirement
{
    public int MinimumAge { get; }

    public MinimumAgeRequirements(int minimumAge)
    {
        MinimumAge = minimumAge;
    }
}
