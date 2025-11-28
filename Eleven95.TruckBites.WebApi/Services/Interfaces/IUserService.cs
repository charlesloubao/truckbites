using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.WebApi.Services.Interfaces;

public interface IUserService
{
    public Task<User?> FindUserByEmailPassword(string email, string password);
}