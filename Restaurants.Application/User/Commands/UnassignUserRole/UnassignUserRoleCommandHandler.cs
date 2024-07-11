using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Application.User.Commands.UnassignUserRole;

public class UnassignUserRoleCommandHandler : IRequestHandler<UnassignUserRoleCommand, string>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public UnassignUserRoleCommandHandler(RoleManager<IdentityRole> roleManager,UserManager<Domain.Entities.User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task<string> Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserEmail);
        if (user == null)
        {
            throw new Exception("User Not found");
        }
        var role = await _roleManager.FindByNameAsync(request.RoleName);
        if (role == null) 
        {
            throw new Exception("Role Not found");
        }
        await _userManager.RemoveFromRoleAsync(user, role.Name!);
        return $"Remove {role.Name} from {user.Email}";
    }
}
