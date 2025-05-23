using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class MetodoPago
    {
        public short Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
