using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Data.Services;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.Vendor.WebApi.Services;

public class OrderFulfillmentService : IOrderFulfillmentService
{
    private readonly ILogger<OrderFulfillmentService> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IUserProvider _userProvider;

    public OrderFulfillmentService(ILogger<OrderFulfillmentService> logger, AppDbContext dbContext,
        IUserProvider userProvider)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userProvider = userProvider;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = await _dbContext.Orders
            .IgnoreQueryFilters()
            .ToListAsync();

        return orders;
    }

    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        var order = await _dbContext.Orders
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(o => o.OrderId == id);

        return order;
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