using FluentAssertions;
using Xunit;

namespace Restaurants.Application.Users.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData("Admin")]
    [InlineData("User")]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test.com", ["Admin", "User"]);

        // Act
        var isinRole = currentUser.IsInRole(roleName);

        // Assert
        isinRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test.com", ["Admin", "User"]);

        // Act
        var isinRole = currentUser.IsInRole("Owner");

        // Assert
        isinRole.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test.com", ["Admin", "User"]);

        // Act
        var isinRole = currentUser.IsInRole("Admin".ToLower());

        // Assert
        isinRole.Should().BeFalse();
    }
}