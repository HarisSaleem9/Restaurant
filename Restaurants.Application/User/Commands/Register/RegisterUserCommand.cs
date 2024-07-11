using MediatR;

namespace Restaurants.Application.User.Commands.Register;

public class RegisterUserCommand : IRequest<string>
{
    public string? email { get; set; }
    public string? phonenumber { get; set; }
}
