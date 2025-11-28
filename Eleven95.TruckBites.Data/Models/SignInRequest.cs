using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class SignInRequest
{
    [Required]
    public required string EmailAddress { get; set; }

    [Required]
    public required string Password { get; set; }
}