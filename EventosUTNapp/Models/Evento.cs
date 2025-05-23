using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [Required]
        public string Lugar { get; set; } = string.Empty;

        [Required]
        public short TipoEventoId { get; set; }
        public TipoEvento? TipoEvento { get; set; }

        public ICollection<Sesion> Sesiones { get; set; } = new List<Sesion>();
        public ICollection<EventoPonente> EventoPonentes { get; set; } = new List<EventoPonente>();
        public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    }
}
