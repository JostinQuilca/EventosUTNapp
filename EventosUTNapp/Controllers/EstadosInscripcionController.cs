using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosInscripcionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EstadosInscripcionController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoInscripcion>>> Get() =>
            await _context.EstadosInscripcion.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoInscripcion>> Get(short id)
        {
            var est = await _context.EstadosInscripcion.FindAsync(id);
            if (est == null) return NotFound();
            return est;
        }

        [HttpPost]
        public async Task<ActionResult<EstadoInscripcion>> Post(EstadoInscripcion est)
        {
            _context.EstadosInscripcion.Add(est);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = est.Id }, est);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, EstadoInscripcion est)
        {
            if (id != est.Id) return BadRequest();
            _context.Entry(est).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.EstadosInscripcion.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var est = await _context.EstadosInscripcion.FindAsync(id);
            if (est == null) return NotFound();
            _context.EstadosInscripcion.Remove(est);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
