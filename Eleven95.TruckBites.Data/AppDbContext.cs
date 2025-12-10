using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.Data;

public class AppDbContext : DbContext
{
    private readonly IUserProvider _userProvider;
    public DbSet<FoodTruck> FoodTrucks { get; set; }
    public DbSet<FoodTruckMenuItem> FoodTruckMenuItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<Payout> Payouts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, IUserProvider userProvider) : base(options)
    {
        this._userProvider = userProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(order => order.Refund)
            .WithOne(refund => refund.Order)
            .HasForeignKey<Refund>(refund => refund.OrderId)
            .IsRequired();
    }
}