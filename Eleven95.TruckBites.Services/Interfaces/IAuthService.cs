using System.Security.Claims;
using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IAuthService
{
    public Task<SignInResponse> SignInAsync();
    public Task SignOutAsync();
}