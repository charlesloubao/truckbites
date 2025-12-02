using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Data.Services;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.WebApi.Services;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly AppDbContext _dbContext;
    private readonly IUserProvider _userProvider;

    public OrderService(AppDbContext dbContext, IPaymentProcessor paymentProcessor, ILogger<OrderService> logger,
        IUserProvider userProvider)
    {
        _dbContext = dbContext;
        _paymentProcessor = paymentProcessor;
        _logger = logger;
        _userProvider = userProvider;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        return await _dbContext.Orders
            .Include(o => o.FoodTruck)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        var existingOrderProcessing =
            await _dbContext.Orders.AnyAsync(o =>
            (
                o.Status == OrderStatus.Created ||
                o.Status == OrderStatus.Processing ||
                o.Status == OrderStatus.PendingPayment ||
                o.Status == OrderStatus.PaymentFailed ||
                o.Status == OrderStatus.PaymentSucceeded
            ) && o.FoodTruckId == request.FoodTruckId);

        if (existingOrderProcessing)
        {
            throw new Exception("An order is already processing for this user and food truck.");
        }

        _logger.LogInformation($"Creating order for food truck {request.FoodTruckId}");

        var order = new Order()
        {
            UserId = _userProvider.GetCurrentUserId()!,
            Amount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            FoodTruckId = request.FoodTruckId,
            Status = OrderStatus.Created
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order> AddItemToOrderAsync(AddItemToOrderRequest request)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
            o.OrderId == request.OrderId && o.Status == OrderStatus.Created);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        var foodTruckMenuItem =
            await _dbContext.FoodTruckMenuItems.FirstOrDefaultAsync(ftmi =>
                ftmi.FoodTruckMenuItemId == request.FoodTruckMenuItemId && ftmi.FoodTruckId == order.FoodTruckId);

        if (foodTruckMenuItem == null)
        {
            throw new Exception("Food truck menu item not found.");
        }

        var existingOrderItem = await _dbContext.OrderItems.FirstOrDefaultAsync(oi =>
            oi.OrderId == request.OrderId && oi.FoodTruckMenuItemId == request.FoodTruckMenuItemId);

        if (existingOrderItem != null)
        {
            throw new Exception("Order item already exists.");
        }

        var orderItem = new OrderItem()
        {
            UserId = _userProvider.GetCurrentUserId()!,
            OrderId = request.OrderId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Quantity = request.Quantity,
            FoodTruckMenuItemId = foodTruckMenuItem.FoodTruckMenuItemId,
            ItemPrice = foodTruckMenuItem.Price,
            ItemName = foodTruckMenuItem.Name
        };

        order.Amount += orderItem.TotalPrice;
        order.UpdatedAt = DateTime.UtcNow;

        _dbContext.OrderItems.Add(orderItem);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Order> PlaceOrderAsync(long orderId)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
                o.OrderId == orderId && o.Status == OrderStatus.Created);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            //TODO: Implement actual logic here

            _logger.LogInformation("Creating payment record for order {OrderOrderId}", order.OrderId);
            var payment = new Payment()
            {
                Amount = order.Amount,
                ExternalId = Guid.NewGuid().ToString(),
                PaymentProcessor = PaymentProcessorType.None,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = PaymentStatus.Success
            };

            //TODO: This most likely needs to be moved to a background job
            _logger.LogInformation("Creating instant payout to food truck {FoodTruckId}", order.FoodTruckId);

            var payout = new Payout()
            {
                Amount = order.Amount,
                ExternalId = Guid.NewGuid().ToString(),
                PaymentProcessor = PaymentProcessorType.None,
                FoodTruckId = order.FoodTruckId,
                Status = PayoutStatus.Completed,
                CreatedDate = DateTime.UtcNow,
                CompletedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            _dbContext.Payouts.Add(payout);

            _logger.LogInformation("Setting order {OrderId} as completed", order.OrderId);
            order.Payment = payment;
            order.Status = OrderStatus.Completed;
            order.UpdatedAt = DateTime.UtcNow;

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}