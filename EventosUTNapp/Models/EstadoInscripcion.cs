using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class EstadoInscripcion
    {
        public short Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
