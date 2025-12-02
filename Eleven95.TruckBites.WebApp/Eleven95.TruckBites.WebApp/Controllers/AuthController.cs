using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.WebApp.Controllers;

[ApiController]
[Route("app/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("signin")]
    public IActionResult SignIn(string? returnUrl = null)
    {
        var redirectUrl = returnUrl ?? Url.Content("~/");
        return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl },
            Auth0Constants.AuthenticationScheme);
    }


    [HttpPost("signout")]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }
}