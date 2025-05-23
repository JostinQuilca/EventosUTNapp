using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoEventosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TipoEventosController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoEvento>>> Get() =>
            await _context.TipoEventos.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoEvento>> Get(short id)
        {
            var tipo = await _context.TipoEventos.FindAsync(id);
            if (tipo == null) return NotFound();
            return tipo;
        }

        [HttpPost]
        public async Task<ActionResult<TipoEvento>> Post(TipoEvento tipo)
        {
            _context.TipoEventos.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = tipo.Id }, tipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, TipoEvento tipo)
        {
            if (id != tipo.Id) return BadRequest();
            _context.Entry(tipo).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.TipoEventos.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var tipo = await _context.TipoEventos.FindAsync(id);
            if (tipo == null) return NotFound();
            _context.TipoEventos.Remove(tipo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
