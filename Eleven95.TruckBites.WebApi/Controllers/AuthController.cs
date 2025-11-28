using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Eleven95.TruckBites.WebApi.Controllers;

// In API Project > Controllers > AuthController.cs

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var appUser = await _userService.FindUserByEmailPassword(request.EmailAddress, request.Password);
        if (appUser == null) return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.UserId.ToString()),
            new Claim(ClaimTypes.Name, appUser.DisplayName),
            new Claim(ClaimTypes.Email, appUser.EmailAddress),
        };

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
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