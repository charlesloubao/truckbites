using Eleven95.TruckBites.Client.Shared.Extensions;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.Vendor.WebApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddAppHttpClients(builder.HostEnvironment.BaseAddress);

builder.Services.AddScoped<IFoodTruckAdminService, FoodTruckAdminService>();
builder.Services.AddScoped<IOrderFulfillmentService, OrderFulfillmentService>();

await builder.Build().RunAsync();