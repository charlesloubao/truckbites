using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class FoodTruckMenuItem
{
    public long FoodTruckMenuItemId { get; set; }

    [Required]
    public long FoodTruckId { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    public bool IsSoldOut { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }
}