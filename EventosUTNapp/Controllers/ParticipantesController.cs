using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ParticipantesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> Get() =>
            await _context.Participantes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> Get(int id)
        {
            var p = await _context.Participantes.FindAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public async Task<ActionResult<Participante>> Post(Participante p)
        {
            _context.Participantes.Add(p);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Participante p)
        {
            if (id != p.Id) return BadRequest();
            _context.Entry(p).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Participantes.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Participantes.FindAsync(id);
            if (p == null) return NotFound();
            _context.Participantes.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
