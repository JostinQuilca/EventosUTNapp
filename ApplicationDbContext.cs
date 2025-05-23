using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Models;  // ajusta tu namespace

namespace EventosUTNapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aquí pones un DbSet por cada entidad:
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Ponente> Ponentes { get; set; }
        public DbSet<EventoPonente> EventoPonentes { get; set; }
        // ...y el resto de tus DbSet<>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la clave primaria compuesta:
            modelBuilder.Entity<EventoPonente>()
                .HasKey(ep => new { ep.EventoId, ep.PonenteId });

            // Si tienes más configuraciones FluentAPI, las pones aquí
        }
    }
}
