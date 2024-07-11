using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand,string>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AssignUserRoleCommandHandler(UserManager<Domain.Entities.User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<string> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserEmail);
        if (user == null)
        {
            throw new Exception("User not found in Role");
        }
        var role = await _roleManager.FindByNameAsync(request.RoleName);
        if (role == null)
        {
            throw new Exception("Role not found in Role");
        }
        await _userManager.AddToRoleAsync(user, role.Name!);
        return $"{role.Name} assigned to {user.Email}";
    }
}
