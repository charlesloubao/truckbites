using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Eleven95.TruckBites.WebApi.Services;

public class FoodTruckService : IFoodTruckService
{
    private readonly IMemoryCache _memoryCache;
    private readonly AppDbContext _dbContext;

    public FoodTruckService(AppDbContext dbContext, IMemoryCache memoryCache)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
    }

    public async Task<List<FoodTruck>> GetAllFoodTrucksAsync()
    {
        const string cacheKey = "AllFoodTrucks";

        if (_memoryCache.TryGetValue(cacheKey, out List<FoodTruck>? cachedFoodTrucks))
        {
            return cachedFoodTrucks!;
        }

        var foodTrucks = await _dbContext.FoodTrucks.ToListAsync();
        _memoryCache.Set(cacheKey, foodTrucks, TimeSpan.FromMinutes(10));

        return foodTrucks;
    }

    public async Task<FoodTruck?> GetFoodTruckByIdAsync(long id)
    {
        var cacheKey = $"FoodTruck_{id}";

        if (_memoryCache.TryGetValue(cacheKey, out FoodTruck? cachedFoodTruck))
        {
            return cachedFoodTruck;
        }

        var foodTruck = await _dbContext.FoodTrucks
            .Include(ft => ft.MenuItems)
            .FirstOrDefaultAsync(ft => ft.FoodTruckId == id);

        if (foodTruck != null)
        {
            _memoryCache.Set(cacheKey, foodTruck, TimeSpan.FromMinutes(1));
        }

        return foodTruck;
    }
}