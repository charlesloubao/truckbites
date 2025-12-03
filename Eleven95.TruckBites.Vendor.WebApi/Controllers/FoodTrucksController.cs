using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.Vendor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodTrucksController : ControllerBase
{
    private readonly IFoodTruckAdminService _foodTruckAdminService;
    
    public FoodTrucksController(IFoodTruckAdminService foodTruckAdminService)
    {
        _foodTruckAdminService = foodTruckAdminService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FoodTruck>>> GetAllFoodTrucks()
    {
        var orders = await _foodTruckAdminService.GetFoodTrucksAsync();
        return Ok(orders);
    }

    [HttpGet("{orderId:long}")]
    public async Task<ActionResult<Order>> GetFoodTruckBy(long orderId)
    {
        var foodtruck = await _foodTruckAdminService.GetFoodTruckByIdAsync(orderId);

        if (foodtruck == null)
        {
            return NotFound();
        }

        return Ok(foodtruck);
    }
}