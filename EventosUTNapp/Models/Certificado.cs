using EventosUTNapp.Models;

public class Certificado
{
    public int Id { get; set; }

    public int InscripcionId { get; set; }

    public DateTime FechaEmision { get; set; }

    public string UrlCertificado { get; set; } = null!; // o usar string?

    public Inscripcion? Inscripcion { get; set; }  // aquí pones ? para que sea anulable
}
