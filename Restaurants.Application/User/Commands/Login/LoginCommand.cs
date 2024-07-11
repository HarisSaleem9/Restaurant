using MediatR;

namespace Restaurants.Application.User.Commands.Login;

public class LoginCommand : IRequest<string>
{
    public string UserName { get; set;}
}
