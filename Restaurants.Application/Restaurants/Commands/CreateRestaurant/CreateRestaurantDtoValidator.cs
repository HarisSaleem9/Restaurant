using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is require");

        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Invalid Category");
        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide a valid Email address");
    }
}
