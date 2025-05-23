using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Models;  // Ajusta el namespace si es necesario

namespace EventosUTNapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
            : base(opts)
        { }

        public DbSet<TipoEvento> TipoEventos { get; set; }
        public DbSet<EstadoInscripcion> EstadosInscripcion { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Ponente> Ponentes { get; set; }
        public DbSet<EventoPonente> EventoPonentes { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // Clave primaria compuesta para la tabla relacional EventoPonente
            mb.Entity<EventoPonente>()
              .HasKey(ep => new { ep.EventoId, ep.PonenteId });

            // Forzar nombres de tablas y columnas a minúsculas para evitar comillas y sensibilidad en PostgreSQL
            foreach (var entity in mb.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (!string.IsNullOrEmpty(tableName))
                {
                    entity.SetTableName(tableName.ToLower());
                }

                foreach (var property in entity.GetProperties())
                {
                    var columnName = property.GetColumnName();
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        property.SetColumnName(columnName.ToLower());
                    }
                }
            }
        }
    }
}
