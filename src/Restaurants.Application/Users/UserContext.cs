using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = (httpContextAccessor?.HttpContext?.User) ?? throw new InvalidOperationException("User context is not present");

        if (user?.Identity == null || !user.Identity.IsAuthenticated)
            return null;

        var userId = user.FindFirst(z => z.Type == ClaimTypes.NameIdentifier).Value;
        var email = user.FindFirst(z => z.Type == ClaimTypes.Email).Value;
        var roles = user.Claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value);


        return new CurrentUser(UserId: userId, Email: email, Roles: roles);

    }
}
