using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InscripcionesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> Get() =>
            await _context.Inscripciones
                          .Include(i => i.Participante)
                          .Include(i => i.Evento)
                          .Include(i => i.Estado)
                          .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcion>> Get(int id)
        {
            var ins = await _context.Inscripciones
                                    .Include(i => i.Participante)
                                    .Include(i => i.Evento)
                                    .Include(i => i.Estado)
                                    .FirstOrDefaultAsync(i => i.Id == id);
            if (ins == null) return NotFound();
            return ins;
        }

        [HttpPost]
        public async Task<ActionResult<Inscripcion>> Post(Inscripcion ins)
        {
            _context.Inscripciones.Add(ins);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = ins.Id }, ins);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Inscripcion ins)
        {
            if (id != ins.Id) return BadRequest();
            _context.Entry(ins).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Inscripciones.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ins = await _context.Inscripciones.FindAsync(id);
            if (ins == null) return NotFound();
            _context.Inscripciones.Remove(ins);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
