using EventosUTNapp.Models;

public class Inscripcion
{
    public int Id { get; set; }

    public int ParticipanteId { get; set; }

    public int EventoId { get; set; }

    public DateTime FechaInscripcion { get; set; }

    public short EstadoId { get; set; }

    public Participante? Participante { get; set; }

    public Evento? Evento { get; set; }

    public EstadoInscripcion? Estado { get; set; }

    public ICollection<Pago>? Pagos { get; set; }

    public Certificado? Certificado { get; set; }  // Aquí pon ? para indicar que puede ser null
}
