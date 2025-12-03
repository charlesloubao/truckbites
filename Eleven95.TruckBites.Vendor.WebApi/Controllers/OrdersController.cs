using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eleven95.TruckBites.Vendor.WebApi.Controllers;

[ApiController]
[Route("api/foodtrucks/{foodtruckId:long}/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderFulfillmentService _orderService;

    public OrdersController(IOrderFulfillmentService orderService)
    {
        _orderService = orderService;
    }

}