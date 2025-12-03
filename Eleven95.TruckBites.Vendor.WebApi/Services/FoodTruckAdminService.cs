using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.Vendor.WebApi.Services;

public class FoodTruckAdminService : IFoodTruckAdminService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<FoodTruckAdminService> _logger;
    public FoodTruckAdminService(ILogger<FoodTruckAdminService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public async Task<List<FoodTruck>?> GetFoodTrucksAsync()
    {
        var foodTrucks = await _dbContext.FoodTrucks.ToListAsync();
        return foodTrucks;
    }

    public Task<FoodTruck?> GetFoodTruckByIdAsync(long orderId)
    {
        throw new NotImplementedException();
    }
}