using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventosUTNapp.Data;
using EventosUTNapp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EventosUTNapp.Controllers
{
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void CargarTiposEventos()
        {
            var tipos = _context.TipoEventos
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Nombre })
                .ToList();
            ViewBag.TipoEventos = tipos;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var eventos = await _context.Eventos.Include(e => e.TipoEvento).ToListAsync();
            return View(eventos);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            CargarTiposEventos();
            return View();
        }

        // POST: Eventos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid)
            {
                // Convertir fecha a UTC antes de guardar
                evento.Fecha = DateTime.SpecifyKind(evento.Fecha, DateTimeKind.Utc);

                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CargarTiposEventos();
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return NotFound();

            CargarTiposEventos();
            return View(evento);
        }

        // POST: Eventos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            if (id != evento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Convertir fecha a UTC antes de guardar
                    evento.Fecha = DateTime.SpecifyKind(evento.Fecha, DateTimeKind.Utc);

                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Eventos.Any(e => e.Id == evento.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            CargarTiposEventos();
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var evento = await _context.Eventos
                .Include(e => e.TipoEvento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null) return NotFound();

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
