using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Eleven95.TruckBites.Web;
using Eleven95.TruckBites.Web.Clients;
using Eleven95.TruckBites.Web.Clients.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IFoodTruckClient>(client =>
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!));

builder.Services.AddScoped<IFoodTruckClient, FoodTruckClient>();
builder.Services.AddHttpClient<IFoodTruckClient, FoodTruckClient>(sp => new HttpClient
    { BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!) });

await builder.Build().RunAsync();