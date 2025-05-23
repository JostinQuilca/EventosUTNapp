using System;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class Pago
    {
        public int Id { get; set; }

        [Required]
        public int InscripcionId { get; set; }
        public Inscripcion? Inscripcion { get; set; }

        [Required]
        public short MetodoPagoId { get; set; }
        public MetodoPago? MetodoPago { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaPago { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Monto { get; set; }
    }
}
