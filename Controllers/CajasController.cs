using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class CajasController : Controller
    {
        private readonly AplicationDbContext _context;

        public CajasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cajas
        public async Task<IActionResult> Index()
        {
            var cajas = await _context.Cajas.Include(c => c.Empleado).ToListAsync();
            return View(cajas);
        }

        // GET: Cajas/Crear
        public IActionResult Crear()
        {
            ViewData["id_Empleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombre");
            return View();
        }

        // POST: Cajas/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Caja caja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_Empleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombre", caja.id_Empleado);
            return View(caja);
        }

        // GET: Cajas/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var caja = await _context.Cajas.FindAsync(id);
            if (caja == null) return NotFound();

            // Corregido a caja.id_Empleado
            ViewData["id_Empleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombre", caja.id_Empleado);
            
            return View(caja);
        }

        // POST: Cajas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Caja caja)
        {
            if (id != caja.id_Caja) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CajaExists(caja.id_Caja)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_Empleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombre", caja.id_Empleado);
            return View(caja);
        }

        // GET: Cajas/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var caja = await _context.Cajas
                .Include(c => c.Empleado)
                .FirstOrDefaultAsync(m => m.id_Caja == id);
            
            if (caja == null) return NotFound();

            return View(caja);
        }

        // POST: Cajas/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var caja = await _context.Cajas.FindAsync(id);
            if (caja != null)
            {
                _context.Cajas.Remove(caja);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cajas/Reporte
        public async Task<IActionResult> Reporte()
        {
            var hoy = DateTime.Today;

            var cajaTop = await _context.Cajas
                .Include(c => c.Empleado)
                .Where(c => c.Fecha_Apertura.Date == hoy)
                .OrderByDescending(c => c.Monto_Final)
                .FirstOrDefaultAsync();

            ViewBag.CajaTop = cajaTop;
            ViewBag.EncargadoCaja = cajaTop?.Empleado?.Nombre ?? "No asignado";
            ViewBag.MontoRecaudado = cajaTop?.Monto_Final ?? 0.00m;
            ViewBag.TotalCajasHoy = await _context.Cajas.CountAsync(c => c.Fecha_Apertura.Date == hoy);

            return View();
        }

        private bool CajaExists(int id)
        {
            return _context.Cajas.Any(e => e.id_Caja == id);
        }
    }
}