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

    public async Task<FoodTruck?> GetFoodTruckByIdAsync(long foodTruckId)
    {
        var foodTruck = await _dbContext.FoodTrucks.Where(f => f.FoodTruckId == foodTruckId)
            .Include(f => f.MenuItems)
            .FirstOrDefaultAsync();

        return foodTruck;
    }

    public async Task<List<FoodTruckMenuItem>> UpdateFoodTruckMenuAsync(long foodTruckFoodTruckId,
        List<FoodTruckMenuItem> foodTruckMenuItems)
    {
        var foodTruck = await GetFoodTruckByIdAsync(foodTruckFoodTruckId);

        if (foodTruck == null)
        {
            throw new Exception("Food Truck not found");
        }

        var toDelete = foodTruck.MenuItems.Where(item =>
            !foodTruckMenuItems.Any(item2 => item2.FoodTruckMenuItemId == item.FoodTruckMenuItemId));

        var toAdd = foodTruckMenuItems.Where(it => it.FoodTruckMenuItemId == -1);

        var toUpdate = foodTruck.MenuItems.Where(item =>
            foodTruckMenuItems.Any(item2 => item.FoodTruckMenuItemId == item2.FoodTruckMenuItemId));

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            foreach (var item in toDelete)
            {
                _dbContext.FoodTruckMenuItems.Remove(item);
            }

            foreach (var item in toUpdate)
            {
                var modifiedItem = foodTruckMenuItems.First(it => it.FoodTruckMenuItemId == item.FoodTruckMenuItemId);

                //TODO: Make sure they are not equal before updating

                item.Name = modifiedItem.Name;
                item.Price = modifiedItem.Price;
                item.IsSoldOut = modifiedItem.IsSoldOut;

                item.UpdatedAt = DateTime.Now;
            }

            foreach (var item in toAdd)
            {
                foodTruck.MenuItems.Add(new FoodTruckMenuItem()
                {
                    Name = item.Name,
                    Price = item.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsSoldOut = item.IsSoldOut
                });
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return foodTruck.MenuItems;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}