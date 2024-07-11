using MediatR;

namespace Restaurants.Application.User.Commands;

public class UpdateUserDetailsCommand : IRequest<string>
{
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }    
}
