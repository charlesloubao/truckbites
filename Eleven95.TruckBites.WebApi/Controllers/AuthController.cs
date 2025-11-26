using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eleven95.TruckBites.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Eleven95.TruckBites.WebApi.Controllers;

// In API Project > Controllers > AuthController.cs

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("signin")]
    public IActionResult SignIn()
    {
        // 1. Validate User (Check DB, etc.)
        //TODO:

        var appUser = new User()
        {
            UserId = 1,
            DisplayName = "Fake User",
            EmailAddress = "user@example.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        // 2. Create Claims (User Data)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.UserId.ToString()),
            new Claim(ClaimTypes.Name, appUser.DisplayName),
            new Claim(ClaimTypes.Email, appUser.EmailAddress),
        };

        // 3. Sign the Token
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Set Expiry
            signingCredentials: creds
        );

        var jwtString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new SignInResponse()
        {
            User = appUser,
            Token = jwtString,
            Success = true
        });
    }
}