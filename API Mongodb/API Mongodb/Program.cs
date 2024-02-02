using SkiServiceManagementApi.Models;
using SkiServiceManagementApi.Services;
using SkiServiceManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Konfigurationsmodell für SkiServiceDatabaseSettings
builder.Services.Configure<SkiServiceDatabaseSettings>(
    builder.Configuration.GetSection("SkiServiceDatabase"));

// Registrieren Sie Ihren SkiServiceManagementService als Singleton
builder.Services.AddSingleton<SkiServiceManagementService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
