using EventosUTNapp.Components;
using EventosUTNapp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión a PostgreSQL
var conn = builder.Configuration.GetConnectionString("Postgres");

// Agregar DbContext con PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseNpgsql(conn));

// Agregar MVC con vistas y Razor Pages (necesario para Blazor Server y fallback)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Agregar API Controllers con configuración JSON para referencias circulares
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Agregar Blazor Server
builder.Services.AddServerSideBlazor();

// Agregar Swagger para documentación API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Swagger middlewares
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventosUTNapp API v1");
    c.RoutePrefix = "swagger";  // Acceder en /swagger
});

// Mapear API Controllers
app.MapControllers();

// Mapear rutas MVC con controlador y vista por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapear Razor Pages (imprescindible para Blazor y fallback)
app.MapRazorPages();

// Mapear Blazor Hub y fallback a la página _Host
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
