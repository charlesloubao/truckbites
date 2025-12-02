using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = await _orderService.CreateOrderAsync(request);
        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Order>> GetOrderById(long id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost("{id}/items")]
    public async Task<ActionResult<Order>> AddItemToOrder(long id, [FromBody] AddItemToOrderRequest request)
    {
        var order = await _orderService.AddItemToOrderAsync(request);
        return Ok(order);
    }

    [HttpPost]
    [Route("{orderId:long}/place-order")]
    public async Task<ActionResult<Order>> ProcessOrder(long orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);

        if (order == null)
        {
            return BadRequest();
        }

        var response = await _orderService.PlaceOrderAsync(orderId);
        return Ok(response);
    }
}