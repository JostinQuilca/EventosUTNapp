using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;

namespace EventosUTNapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CertificadosController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certificado>>> Get() =>
            await _context.Certificados
                          .Include(c => c.Inscripcion)
                          .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Certificado>> Get(int id)
        {
            var cert = await _context.Certificados
                                     .Include(c => c.Inscripcion)
                                     .FirstOrDefaultAsync(c => c.Id == id);
            if (cert == null) return NotFound();
            return cert;
        }

        [HttpPost]
        public async Task<ActionResult<Certificado>> Post(Certificado cert)
        {
            _context.Certificados.Add(cert);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cert.Id }, cert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Certificado cert)
        {
            if (id != cert.Id) return BadRequest();
            _context.Entry(cert).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) when (!_context.Certificados.Any(e => e.Id == id))
            { return NotFound(); }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cert = await _context.Certificados.FindAsync(id);
            if (cert == null) return NotFound();
            _context.Certificados.Remove(cert);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
