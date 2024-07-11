using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.User.Commands;

public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand,string  >
{
    private readonly IUserContext _userContext;
    private readonly IUserStore<Domain.Entities.User> _userStore;

    public UpdateUserDetailsCommandHandler(IUserContext userContext,IUserStore<Domain.Entities.User> userStore)
    {
        _userContext = userContext;
        _userStore = userStore;
    }
    public async Task<string> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = _userContext.GetCurrentUser();
        var dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken);
        if(dbUser==null)
        {
            throw new ArgumentException("User Not found");
        }
        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await _userStore.UpdateAsync(dbUser,cancellationToken);
        return "User Updated";

    }
}
