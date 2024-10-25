using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "La mejor API",
        Version = "2023-1193",
        Description = "Cristopher"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cristopher API 2023-1193");
        c.RoutePrefix = string.Empty; // Para que Swagger se cargue en la raíz
    });
}

// Endpoints

app.MapGet("/Saludo", (string name) => $"Hola {name}");

//Metodos de la tarea 

app.MapGet("/noticias", () => Paso1.Ejecutar());
app.MapPost("/registro_Usuario", ([FromBody] Usuario u) => ManejadorUsuario.Registro(u));
app.MapPost("/iniciar_sesion", (DatosLogin dl) => ManejadorUsuario.IniciarSecion(dl));
app.MapPost("/registro_Incidencia", (Incidencia Ic) => ManejadorUsuario.RegistroIncidencia(Ic));
app.MapGet("/clima", async (string placeId) => 
{
    return await Clima.ObtenerClimaAsync(placeId);
});


app.Run();