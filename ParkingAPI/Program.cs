using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using ParkingAPI.Services;
using ParkingAPI.Middleware;
using ParkingAPI.Repositories; // ✅ IMPORTANTE!

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HealthChecks
builder.Services.AddHealthChecks();

// Versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
});

// ✅ Services com interfaces
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MLService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// API Key Middleware
app.UseMiddleware<ApiKeyMiddleware>();

// Health Check Endpoint
app.MapHealthChecks("/health");

// Map Controllers
app.MapControllers();

app.Run();

public partial class Program { }
