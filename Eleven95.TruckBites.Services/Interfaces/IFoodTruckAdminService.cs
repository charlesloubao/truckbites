using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IFoodTruckAdminService
{
    Task<List<FoodTruck>?> GetFoodTrucksAsync();
    Task<FoodTruck?> GetFoodTruckByIdAsync(long foodTruckId);
    Task<List<FoodTruckMenuItem>> UpdateFoodTruckMenuAsync(long foodTruckFoodTruckId,
        List<FoodTruckMenuItem> foodTruckMenuItems);
}