using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.Vendor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodTrucksController : ControllerBase
{
    private readonly IFoodTruckAdminService _foodTruckAdminService;
    private readonly IOrderFulfillmentService _orderService;

    public FoodTrucksController(IFoodTruckAdminService foodTruckAdminService, IOrderFulfillmentService orderService)
    {
        _foodTruckAdminService = foodTruckAdminService;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FoodTruck>>> GetAllFoodTrucks()
    {
        var orders = await _foodTruckAdminService.GetFoodTrucksAsync();
        return Ok(orders);
    }

    [HttpGet("{foodTruckId:long}")]
    public async Task<ActionResult<Order>> GetFoodTruckBy(long foodTruckId)
    {
        var foodtruck = await _foodTruckAdminService.GetFoodTruckByIdAsync(foodTruckId);

        if (foodtruck == null)
        {
            return NotFound();
        }

        return Ok(foodtruck);
    }
    
    [HttpPut("{foodTruckId:long}/menu")]
    public async Task<ActionResult<Order>> UpdateFoodTruckMenu(long foodTruckId, [FromBody] List<FoodTruckMenuItem> foodTruckMenuItems)
    {
        var menu = await _foodTruckAdminService.UpdateFoodTruckMenuAsync(foodTruckId, foodTruckMenuItems);
        return Ok(menu);
    }


    [HttpGet("{foodtruckId:long}/orders")]
    public async Task<ActionResult<List<Order>>> GetAllOrders(long foodtruckId)
    {
        var orders = await _orderService.GetOrdersForFoodTruck(foodtruckId);
        return Ok(orders);
    }

    [HttpGet("{foodtruckId:long}/orders/{orderId:long}")]
    public async Task<ActionResult<Order>> GetOrderById(long foodtruckId, long orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(foodtruckId, orderId);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost("{foodtruckId:long}/orders/{orderId:long}/confirm")]
    public async Task<ActionResult<Order>> ConfirmOrder(long foodtruckId, long orderId)
    {
        var order = await _orderService.ConfirmOrderAsync(foodtruckId, orderId);
        return Ok(order);
    }

    [HttpPost("{foodtruckId:long}/orders/{orderId:long}/complete")]
    public async Task<ActionResult<Order>> CompleteOrder(long foodtruckId, long orderId)
    {
        var order = await _orderService.CompleteOrderAsync(foodtruckId, orderId);
        return Ok(order);
    }

    [HttpPost("{foodtruckId:long}/orders/{orderId:long}/cancel")]
    public async Task<ActionResult<Order>> CancelOrder(long foodtruckId, long orderId)
    {
        var order = await _orderService.CancelOrderAsync(foodtruckId, orderId);
        return Ok(order);
    }
}