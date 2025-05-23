using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class TipoEvento
    {
        public short Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Evento> Eventos { get; set; } = new List<Evento>();
    }
}
