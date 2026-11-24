using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class VentasController : Controller
    {
        private readonly AplicationDbContext _context;

        public VentasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.Caja)
                .ToListAsync();
            return View(ventas);
        }

        // GET: Ventas/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Ventas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Venta venta)
        {
            // Asignación automática de la hora actual si viene vacía para evitar restricciones de SQLite
            if (venta.Hora == default)
            {
                venta.Hora = DateTime.Now.TimeOfDay;
            }

            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
        }

        // GET: Ventas/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Venta venta)
        {
            if (id != venta.id_Venta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.id_Venta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
        }

        // GET: Ventas/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.Caja)
                .FirstOrDefaultAsync(m => m.id_Venta == id);
            
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Ventas/Reporte
        public async Task<IActionResult> Reporte()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .Include(v => v.Caja)
                .ToListAsync();

            if (ventas == null || !ventas.Any())
            {
                ViewBag.Mensaje = "No hay ventas registradas para generar analíticas.";
                return View(null);
            }

            // Obtiene la venta con el monto total más alto
            var ventaTop = ventas.OrderByDescending(v => v.Total).FirstOrDefault();
            
            // Estadísticas adicionales
            ViewBag.TotalVentas = ventas.Count;
            ViewBag.MontoTotalGeneral = ventas.Sum(v => v.Total);
            ViewBag.PromedioVentas = ventas.Average(v => v.Total);

            return View(ventaTop);
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.id_Venta == id);
        }
    }
}