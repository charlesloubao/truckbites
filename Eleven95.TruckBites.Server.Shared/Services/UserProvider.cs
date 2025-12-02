using System.Security.Claims;
using Eleven95.TruckBites.Data.Services;
using Microsoft.AspNetCore.Http;

namespace Eleven95.TruckBites.Server.Shared.Services;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        return userIdClaim?.Value;
    }
}