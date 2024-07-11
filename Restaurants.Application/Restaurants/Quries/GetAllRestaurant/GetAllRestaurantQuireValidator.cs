using FluentValidation;

namespace Restaurants.Application.Restaurants.Quries.GetAllRestaurant;

public class GetAllRestaurantQuireValidator : AbstractValidator<GetAllRestaurantQuire>
{
    private int[] allowPageSize = new int[] { 5, 10, 15, 30 };
    public GetAllRestaurantQuireValidator()
    {
        RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageNumber)
            .Must(value => allowPageSize.Contains(value))
            .WithMessage($"page Size must be in {string.Join(",",allowPageSize)}");
    }
}
