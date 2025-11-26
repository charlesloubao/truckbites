using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTrucksController : ControllerBase
    {
        private readonly IFoodTruckService _foodTruckService;

        public FoodTrucksController(IFoodTruckService foodTruckService)
        {
            _foodTruckService = foodTruckService;
        }

        [HttpGet]
        public async Task<List<FoodTruck>> GetAll()
        {
            return await _foodTruckService.GetAllFoodTrucksAsync();
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<FoodTruck>> GetById(long id)
        {
            var foodTruck = await _foodTruckService.GetFoodTruckByIdAsync(id);

            if (foodTruck == null)
            {
                return NotFound();
            }

            return foodTruck;
        }
    }
}