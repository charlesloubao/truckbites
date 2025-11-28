using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class FoodTruck
{
    public long FoodTruckId { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    [Required] public DateTime UpdatedAt { get; set; }

    [Required] public required string DisplayName { get; set; }

    [Required] public required string Description { get; set; }

    public List<FoodTruckMenuItem> MenuItems { get; set; }
    
    public List<Order> Orders { get; set; }
}