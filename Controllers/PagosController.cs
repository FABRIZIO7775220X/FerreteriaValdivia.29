using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class PagosController : Controller
    {
        private readonly AplicationDbContext _context;

        public PagosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pagos
        public async Task<IActionResult> Index()
        {
            var pagos = await _context.Pagos.ToListAsync();
            return View(pagos);
        }

        // GET: Pagos/Reporte (Analítica de Pagos)
        public async Task<IActionResult> Reporte()
        {
            var pagos = await _context.Pagos.ToListAsync();

            var metodoMasUtilizado = pagos
                .GroupBy(p => p.MetodoPago ?? "No especificado")
                .Select(g => new { Metodo = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .FirstOrDefault();

            ViewBag.MetodoFrecuente = metodoMasUtilizado != null 
                ? $"{metodoMasUtilizado.Metodo} ({metodoMasUtilizado.Cantidad} transacciones)" 
                : "Sin transacciones registradas";

            ViewBag.TotalPagos = pagos.Count;
            ViewBag.MontoTotal = pagos.Sum(p => p.Monto);

            return View(pagos);
        }

        // GET: Pagos/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Pagos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Pago pago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }

        // GET: Pagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();

            return View("Editar", pago); // <-- Asegúrate de pasar "Editar" aquí
        }

        // POST: Pagos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pago pago)
        {
            if (id != pago.id_Pago) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.id_Pago)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Editar", pago); // <-- Y aquí también
        }

        // GET: Pagos/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var pago = await _context.Pagos
                .FirstOrDefaultAsync(m => m.id_Pago == id);

            if (pago == null) return NotFound();

            return View(pago);
        }

        // POST: Pagos/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id_Pago)
        {
            var pago = await _context.Pagos.FindAsync(id_Pago);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.id_Pago == id);
        }
    }
}