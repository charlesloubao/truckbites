using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.WebApp.Controllers;

[ApiController]
[Route("app/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        Console.WriteLine(_authService.GetType());
        var response = await _authService.SignInWithEmailAndPassword(request);

        if (!response.Success)
        {
            return BadRequest(new APIResponse() { Success = false, Message = response.Message });
        }

        var jwt = response.Token!;

        // 2. Decode the JWT to get User Claims (Optional but recommended for UI logic)
        // You can use System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(jwt);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = jwtToken.ValidTo // Sync cookie expiry with Token expiry
        };

        // This saves the token into the encrypted cookie blob
        authProperties.StoreTokens(new[]
        {
            new AuthenticationToken { Name = "access_token", Value = jwt }
        });

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, response.User!.DisplayName),
            new Claim(ClaimTypes.Email, response.User!.EmailAddress),
            new Claim(ClaimTypes.NameIdentifier, response.User!.UserId.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authProperties);

        return Ok(response.User);
    }

    [HttpPost("signout")]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}