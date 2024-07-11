using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.User;

namespace Restaurants.Infrastructure.Authorizarion.Requirements;

public class MinimumAgeRequirementsHandler : AuthorizationHandler<MinimumAgeRequirements>
{
    private readonly IUserContext _userContext;

    public MinimumAgeRequirementsHandler(IUserContext userContext)
    {
        _userContext = userContext;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirements requirement)
    {
        var currentUser = _userContext.GetCurrentUser();

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(); 
        }
        return Task.CompletedTask;  
    }
}
