using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;

namespace Eleven95.TruckBites.Vendor.WebApi.Services;

public class OrderFulfillmentService : IOrderFulfillmentService
{
    public Task<List<Order>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Order> ConfirmOrderAsync(long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CancelOrderAsync(long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CompleteOrderAsync(long orderId)
    {
        throw new NotImplementedException();
    }
}