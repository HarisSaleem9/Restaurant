using MediatR;

namespace Restaurants.Application.User.Commands.UnassignUserRole;

public class UnassignUserRoleCommand : IRequest<string>
{
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
