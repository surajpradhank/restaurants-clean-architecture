using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Application.Users.Tests;
public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier,"1"),
            new (ClaimTypes.Email,"test@test.com"),
            new (ClaimTypes.Role,"Admin"),
            new (ClaimTypes.Role,"User"),
            new ("Nationality","Indian")
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);
        // Act

        var currentUser = userContext.GetCurrentUser();

        // Assert
        currentUser.Should().NotBeNull();
        currentUser?.UserId.Should().Be("1");
        currentUser?.Email.Should().Be("test@test.com");
        currentUser?.Roles.Should().ContainInOrder("Admin", "User");
    }

    [Fact]
    public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);
        Action action = () => userContext.GetCurrentUser();

        action.Should().Throw<InvalidOperationException>();
    }
}
