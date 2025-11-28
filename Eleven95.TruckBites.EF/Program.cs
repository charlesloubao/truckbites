using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IUserProvider, UserProvider>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!,
        b => b.MigrationsAssembly("Eleven95.TruckBites.EF"))
);

var host = builder.Build();

await host.RunAsync();

public class UserProvider : IUserProvider
{
    public long? GetCurrentUserId()
    {
        return 0;
    }
}