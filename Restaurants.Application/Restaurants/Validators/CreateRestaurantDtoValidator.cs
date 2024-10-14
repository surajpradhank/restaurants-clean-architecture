using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> validCategories = ["Italian", "Indian", "Chinese"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(6, 100);

        RuleFor(dto => dto.Description)
           .NotEmpty().WithMessage("Description is required");

        RuleFor(dto => dto.ContactEmail)
          .EmailAddress().WithMessage("Please provide valid email address");

        RuleFor(dto => dto.ContactNumber)
          .Length(10).WithMessage("Please provide valid phone number");

        RuleFor(dto => dto.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid Category, Please choose from valid categories.");

    }
}
