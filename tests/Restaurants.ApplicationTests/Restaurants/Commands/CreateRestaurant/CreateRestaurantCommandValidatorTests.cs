using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void CreateRestaurantValidator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test aderan",
            Description = "Sample description",
            Category = "Italian",
            ContactEmail = "test@test.com",
            ContactNumber = "1234567890",
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void CreateRestaurantValidator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Description = "",
            Category = "Ita",
            ContactEmail = "@test.com",
            PostalCode = "123456789",
        };
        var validator = new CreateRestaurantCommandValidator();
        // Act
        var result = validator.TestValidate(command);
        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Description);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
    }

    [Theory()]
    [InlineData("Italian")]
    [InlineData("Indian")]
    [InlineData("Chinese")]
    public void CreateRestaurantValidator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // Arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };
        // Act
        var result = validator.TestValidate(command);
        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }
}