using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.Vendor.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl },
            Auth0Constants.AuthenticationScheme);
    }


    [HttpGet("logout")]
    public async Task<IActionResult> Logout(string returnUrl = "/account/logged-out")
    {
        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect(returnUrl);
    }
}