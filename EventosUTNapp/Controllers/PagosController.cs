using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PagosController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> Get() =>
            await _context.Pagos
                          .Include(p => p.Inscripcion)
                          .Include(p => p.MetodoPago)
                          .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> Get(int id)
        {
            var pago = await _context.Pagos
                                     .Include(p => p.Inscripcion)
                                     .Include(p => p.MetodoPago)
                                     .FirstOrDefaultAsync(p => p.Id == id);
            if (pago == null) return NotFound();
            return pago;
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> Post(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pago.Id }, pago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Pago pago)
        {
            if (id != pago.Id) return BadRequest();
            _context.Entry(pago).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Pagos.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();
            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
