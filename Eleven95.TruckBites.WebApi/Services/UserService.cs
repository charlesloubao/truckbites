using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.WebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.WebApi.Services;

public class UserService:IUserService
{
    private readonly AppDbContext _dbContext;
    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> FindUserByEmailPassword(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);
        return user;
    }
}