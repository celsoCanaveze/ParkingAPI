using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ParkingAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

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

// Custom Services
builder.Services.AddScoped<ReservaService>();
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
