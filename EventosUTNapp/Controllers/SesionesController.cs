using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SesionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SesionesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sesion>>> Get() =>
            await _context.Sesiones
                          .Include(s => s.Evento)
                          .Include(s => s.Sala)
                          .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Sesion>> Get(int id)
        {
            var ses = await _context.Sesiones
                                    .Include(s => s.Evento)
                                    .Include(s => s.Sala)
                                    .FirstOrDefaultAsync(s => s.Id == id);
            if (ses == null) return NotFound();
            return ses;
        }

        [HttpPost]
        public async Task<ActionResult<Sesion>> Post(Sesion ses)
        {
            _context.Sesiones.Add(ses);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = ses.Id }, ses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Sesion ses)
        {
            if (id != ses.Id) return BadRequest();
            _context.Entry(ses).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Sesiones.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ses = await _context.Sesiones.FindAsync(id);
            if (ses == null) return NotFound();
            _context.Sesiones.Remove(ses);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
