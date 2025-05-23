using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PonentesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PonentesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ponente>>> Get() =>
            await _context.Ponentes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Ponente>> Get(int id)
        {
            var p = await _context.Ponentes.FindAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public async Task<ActionResult<Ponente>> Post(Ponente p)
        {
            _context.Ponentes.Add(p);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Ponente p)
        {
            if (id != p.Id) return BadRequest();
            _context.Entry(p).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Ponentes.Any(x => x.Id == id)) { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _context.Ponentes.FindAsync(id);
            if (p == null) return NotFound();
            _context.Ponentes.Remove(p);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
