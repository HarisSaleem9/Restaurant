using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Application.User.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IUserStore<Domain.Entities.User> _userStore;

    public RegisterUserCommandHandler(IUserStore<Domain.Entities.User> userStore)
    {
        _userStore = userStore;
    }
    async Task<string> IRequestHandler<RegisterUserCommand, string>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User? user = new Domain.Entities.User()
        {
            Email = request.email,
            PhoneNumber = request.phonenumber
        };
        await _userStore.CreateAsync(user, cancellationToken);
        return "Registeration Completed";
    }
}
