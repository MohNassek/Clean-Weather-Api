using Microsoft.Extensions.DependencyInjection;
using WeatherApi.Infrastructure;
using WeatherApi.Application;
using WeatherApi.Domain.Interfaces;
using WeatherApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Ensure configuration is available
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Register services
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructureDI(connectionString);

// Register Application DI services (includes MediatR)
builder.Services.AddApplicationDI();

// Register HttpClient for WeatherApiService
builder.Services.AddHttpClient<IWeatherApiService, WeatherApiService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
