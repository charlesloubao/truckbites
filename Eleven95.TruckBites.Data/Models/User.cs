using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class User
{
    public string UserId { get; set; }
    [Required] public required string EmailAddress { get; set; }
    [Required] public required string DisplayName { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }

    public List<Order> Orders { get; set; }
}