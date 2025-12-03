using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IOrderFulfillmentService
{
    public Task<List<Order>> GetOrdersForFoodTruck(long foodTruckId);
    public Task<Order?> GetOrderByIdAsync(long foodTruckId, long id);
    public Task<Order> ConfirmOrderAsync(long foodTruckId, long orderId);
    public Task<Order> CancelOrderAsync(long foodTruckId, long orderId);
    public Task<Order> CompleteOrderAsync(long foodTruckId, long orderId);
}