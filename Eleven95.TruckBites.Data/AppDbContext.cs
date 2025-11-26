using Eleven95.TruckBites.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.Data;

public class AppDbContext : DbContext
{
    public DbSet<FoodTruck> FoodTrucks { get; set; }
    public DbSet<FoodTruckMenuItem> FoodTruckMenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}