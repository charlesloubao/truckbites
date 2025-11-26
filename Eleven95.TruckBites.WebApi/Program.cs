using System.Text;
using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Auth

// 1. Get Settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

// 2. Add Authentication Services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

            // Optional: Remove default 5 min clock skew if you want strict expiration
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenApi();
}

// This cache is used to persist state across prerendering.
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!)
);

builder.Services.AddScoped<IFoodTruckService, FoodTruckService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentProcessor, StripePaymentProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();