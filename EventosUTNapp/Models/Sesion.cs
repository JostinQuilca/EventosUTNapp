using System;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class Sesion
    {
        public int Id { get; set; }

        [Required]
        public int EventoId { get; set; }
        public Evento? Evento { get; set; }

        [Required]
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime HoraInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime HoraFin { get; set; }
    }
}
