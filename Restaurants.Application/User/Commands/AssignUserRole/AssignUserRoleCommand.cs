using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommand : IRequest<string>
{
    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
