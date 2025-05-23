using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MetodosPagoController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodoPago>>> Get() =>
            await _context.MetodosPago.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoPago>> Get(short id)
        {
            var mp = await _context.MetodosPago.FindAsync(id);
            if (mp == null) return NotFound();
            return mp;
        }

        [HttpPost]
        public async Task<ActionResult<MetodoPago>> Post(MetodoPago mp)
        {
            _context.MetodosPago.Add(mp);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = mp.Id }, mp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, MetodoPago mp)
        {
            if (id != mp.Id) return BadRequest();
            _context.Entry(mp).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.MetodosPago.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var mp = await _context.MetodosPago.FindAsync(id);
            if (mp == null) return NotFound();
            _context.MetodosPago.Remove(mp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
