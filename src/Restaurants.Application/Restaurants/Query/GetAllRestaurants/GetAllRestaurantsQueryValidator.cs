using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{

    private readonly int[] allowedPageSizes = [1, 2, 5, 10, 15, 30];
    private readonly string[] allowedSortByColumnNames = [nameof(RestaurantDto.Name),
                                                          nameof(RestaurantDto.Category),
                                                          nameof(RestaurantDto.Description)];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).Must(value => allowedPageSizes.Contains(value))
                              .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");
        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}
