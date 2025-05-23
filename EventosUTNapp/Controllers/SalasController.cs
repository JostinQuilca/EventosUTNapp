using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SalasController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> Get() =>
            await _context.Salas.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> Get(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();
            return sala;
        }

        [HttpPost]
        public async Task<ActionResult<Sala>> Post(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = sala.Id }, sala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Sala sala)
        {
            if (id != sala.Id) return BadRequest();
            _context.Entry(sala).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Salas.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();
            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
