using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.Vendor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderFulfillmentService _orderService;
    
    public OrdersController(IOrderFulfillmentService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{orderId:long}")]
    public async Task<ActionResult<Order>> GetOrderById(long orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }
}