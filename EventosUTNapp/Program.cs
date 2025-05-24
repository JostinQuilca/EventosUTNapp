using EventosUTNapp.Components;
using EventosUTNapp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- 1.  Configuración de servicios ----------
string conn = builder.Configuration.GetConnectionString("Postgres")!;

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseNpgsql(conn));

// MVC + Razor Pages  (necesario para vistas MVC y Blazor Server)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// API Controllers con JSON que admite referencias circulares
builder.Services.AddControllers()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.Preserve;
            o.JsonSerializerOptions.DefaultIgnoreCondition =
                System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });

// Blazor Server
builder.Services.AddServerSideBlazor();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- 2.  Middleware común ----------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ---------- 3.  Swagger ----------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventosUTNapp API v1");
    c.RoutePrefix = "swagger";
});

// ---------- 4.  Endpoints ----------

// 4.1 — API Controllers
app.MapControllers();

// 4.2 — Redirección explícita “/” → “/Home”  (para evitar página vacía)
app.MapGet("/", ctx =>
{
    ctx.Response.Redirect("/Home", permanent: false);
    return Task.CompletedTask;
});

// 4.3 — Rutas MVC (Home / Eventos / etc.)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 4.4 — Razor Pages (Blazor usa esta infraestructura)
app.MapRazorPages();

// 4.5 — Blazor Hub
app.MapBlazorHub();

// 4.6 — Fallback: cualquier ruta no atendida arriba carga Blazor (_Host.cshtml)
app.MapFallbackToPage("/_Host");

// ---------- 5.  Run ----------
app.Run();
