using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoPonentesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventoPonentesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoPonente>>> Get() =>
            await _context.EventoPonentes
                          .Include(ep => ep.Evento)
                          .Include(ep => ep.Ponente)
                          .ToListAsync();

        [HttpGet("{eventoId}/{ponenteId}")]
        public async Task<ActionResult<EventoPonente>> Get(int eventoId, int ponenteId)
        {
            var ep = await _context.EventoPonentes
                                   .FindAsync(eventoId, ponenteId);
            if (ep == null) return NotFound();
            return ep;
        }

        [HttpPost]
        public async Task<ActionResult<EventoPonente>> Post(EventoPonente ep)
        {
            _context.EventoPonentes.Add(ep);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get),
                new { eventoId = ep.EventoId, ponenteId = ep.PonenteId }, ep);
        }

        [HttpDelete("{eventoId}/{ponenteId}")]
        public async Task<IActionResult> Delete(int eventoId, int ponenteId)
        {
            var ep = await _context.EventoPonentes.FindAsync(eventoId, ponenteId);
            if (ep == null) return NotFound();
            _context.EventoPonentes.Remove(ep);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
