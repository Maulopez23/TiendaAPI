using Microsoft.EntityFrameworkCore;
using TiendaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<TiendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TiendaConnection")));

builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para documentar la API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Tienda API",
        Version = "v1",
        Description = "API for managing a store's inventory and sales."
    });
});

//Politica de CORS para permitir todas las solicitudes

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow ALL",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection(); // Redirección de solicitudes HTTP a HTTPS

app.MapControllers(); // Mapeo de controladores para manejar las solicitudes HTTP

app.UseCors("Allow ALL");  // Aplicar la política de CORS definida anteriormente


app.Run();


