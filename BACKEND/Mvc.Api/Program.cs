using DbModel.mecanica;
using Microsoft.EntityFrameworkCore;
using Mvc.Bussnies.Vehiculo;
using Mvc.Repository.VehiculoRepo.Contratos;
using Mvc.Repository.VehiculoRepo.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_mecanicaContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("mecanico_1");
    // Evita AutoDetect (que abre una conexión en startup). Usa la versión conocida del servidor.
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.45-mysql"));
});

// Inyección de dependencias - Repositories
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();

// Inyección de dependencias - Business Logic
builder.Services.AddScoped<IVehiculoBussnies, VehiculoBussnies>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
