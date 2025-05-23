using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class Sala
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Ubicacion { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacidad { get; set; }
    }
}
