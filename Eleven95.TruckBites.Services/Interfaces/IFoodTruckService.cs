using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IFoodTruckService
{
    Task<List<FoodTruck>> GetAllFoodTrucksAsync();
    Task<FoodTruck?> GetFoodTruckByIdAsync(long id);
}