using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventosUTNapp.Models
{
    public class Ponente
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Biografia { get; set; }

        public ICollection<EventoPonente> EventoPonentes { get; set; } = new List<EventoPonente>();
    }
}
