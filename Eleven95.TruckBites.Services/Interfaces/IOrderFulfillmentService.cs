using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IOrderFulfillmentService
{
    public Task<List<Order>> GetOrdersAsync();
    public Task<Order?> GetOrderByIdAsync(long id);
    public Task<Order> ConfirmOrderAsync(long orderId);
    public Task<Order> CancelOrderAsync(long orderId);
    public Task<Order> CompleteOrderAsync(long orderId);
}