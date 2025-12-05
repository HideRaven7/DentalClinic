using DentalClinic.Data;
using DentalClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Controllers
{
    public class QuejaController : Controller
    {
        private readonly DentalClinicContext _context;

        public QuejaController(DentalClinicContext context)
        {
            _context = context;
        }

        // GET: Queja
        public async Task<IActionResult> Index()
        {
            var quejas = await _context.Queja
                .Include(q => q.Cliente)
                .Include(q => q.Servicio)
                .Include(q => q.Reserva)
                .ToListAsync();
            return View(quejas);
        }

        // GET: Queja/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var queja = await _context.Queja
                .Include(q => q.Cliente)
                .Include(q => q.Servicio)
                .Include(q => q.Reserva)
                .FirstOrDefaultAsync(q => q.Id == id);
            
            return queja == null ? NotFound() : View(queja);
        }

        // GET: Queja/Create
        public async Task<IActionResult> Create(int? reservaId)
        {
            var queja = new Queja();
            
            if (reservaId.HasValue)
            {
                var reserva = await _context.Reserva
                    .Include(r => r.Cliente)
                    .Include(r => r.Servicio)
                    .FirstOrDefaultAsync(r => r.Id == reservaId);

                if (reserva != null)
                {
                    queja.ReservaId = reserva.Id;
                    queja.ClienteId = reserva.ClienteId;
                    queja.ServicioId = reserva.ServicioId;
                }
            }

            ViewData["ClienteId"] = await _context.Cliente.ToListAsync();
            ViewData["ServicioId"] = await _context.Servicio.ToListAsync();
            ViewData["ReservaId"] = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .ToListAsync();

            return View(queja);
        }

        // POST: Queja/Create
        [HttpPost]
        public async Task<IActionResult> Create(Queja queja)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = await _context.Cliente.ToListAsync();
                ViewData["ServicioId"] = await _context.Servicio.ToListAsync();
                ViewData["ReservaId"] = await _context.Reserva
                    .Include(r => r.Cliente)
                    .Include(r => r.Servicio)
                    .ToListAsync();
                return View(queja);
            }

            queja.FechaCreacion = DateTime.Now;
            _context.Queja.Add(queja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Queja/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var queja = await _context.Queja.FindAsync(id);
            if (queja == null)
                return NotFound();

            ViewData["ClienteId"] = await _context.Cliente.ToListAsync();
            ViewData["ServicioId"] = await _context.Servicio.ToListAsync();
            ViewData["ReservaId"] = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .ToListAsync();

            return View(queja);
        }

        // POST: Queja/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Queja queja)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = await _context.Cliente.ToListAsync();
                ViewData["ServicioId"] = await _context.Servicio.ToListAsync();
                ViewData["ReservaId"] = await _context.Reserva
                    .Include(r => r.Cliente)
                    .Include(r => r.Servicio)
                    .ToListAsync();
                return View(queja);
            }

            _context.Queja.Update(queja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Queja/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var queja = await _context.Queja
                .Include(q => q.Cliente)
                .Include(q => q.Servicio)
                .FirstOrDefaultAsync(q => q.Id == id);
            
            return queja == null ? NotFound() : View(queja);
        }

        // POST: Queja/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var queja = await _context.Queja.FindAsync(id);

            if (queja != null)
            {
                _context.Queja.Remove(queja);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
