using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class DetalleVentasController : Controller
    {
        private readonly AplicationDbContext _context;

        public DetalleVentasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalleVentas
        public async Task<IActionResult> Index()
        {
            var ferreteriaContext = _context.DetalleVentas
                .Include(d => d.Producto)
                .Include(d => d.Venta);
                
            return View(await ferreteriaContext.ToListAsync());
        }

        // GET: DetalleVentas/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: DetalleVentas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(DetalleVenta detalleVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var detalleVenta = await _context.DetalleVentas.FindAsync(id);
            if (detalleVenta == null) return NotFound();

            return View(detalleVenta);
        }

        // POST: DetalleVentas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.id_DetalleVenta) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentaExists(detalleVenta.id_DetalleVenta)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(detalleVenta);
        }

        // GET: DetalleVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.Venta)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.id_DetalleVenta == id);

            if (detalleVenta == null) return NotFound();

            return View(detalleVenta);
        }

        // GET: DetalleVentas/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var detalleVenta = await _context.DetalleVentas
                .Include(d => d.Venta)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.id_DetalleVenta == id);

            if (detalleVenta == null) return NotFound();

            return View(detalleVenta);
        }

        // POST: DetalleVentas/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id_DetalleVenta)
        {
            var detalleVenta = await _context.DetalleVentas.FindAsync(id_DetalleVenta);
            if (detalleVenta != null)
            {
                _context.DetalleVentas.Remove(detalleVenta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: DetalleVentas/Reporte
        public async Task<IActionResult> Reporte()
        {
            var detalles = await _context.DetalleVentas
                .Include(d => d.Venta)
                .Include(d => d.Producto)
                .ToListAsync();

            if (detalles == null || !detalles.Any())
            {
                ViewBag.Mensaje = "No hay registros de detalle de ventas para generar analíticas.";
                return View(null);
            }

            // Obtiene el detalle con la mayor cantidad de unidades vendidas
            var detalleTop = detalles.OrderByDescending(d => d.Cantidad).FirstOrDefault();
            
            // Métricas y estadísticas adicionales
            ViewBag.TotalRegistros = detalles.Count;
            ViewBag.SumaTotalUnidades = detalles.Sum(d => d.Cantidad);
            ViewBag.IngresoTotalDetalles = detalles.Sum(d => d.Subtotal);

            return View(detalleTop);
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetalleVentas.Any(e => e.id_DetalleVenta == id);
        }
    }
}