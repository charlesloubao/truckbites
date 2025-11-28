using System.Security.Claims;
using Eleven95.TruckBites.Data.Services;

namespace Eleven95.TruckBites.WebApi.Services;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return userIdClaim != null ? long.Parse(userIdClaim!.Value) : null;
    }
}