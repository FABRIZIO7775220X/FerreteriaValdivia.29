using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly AplicationDbContext _context;

        public ProveedoresController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            var proveedores = await _context.Proveedores.ToListAsync();
            return View(proveedores);
        }

        // GET: Proveedores/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Proveedores/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedores/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        // POST: Proveedores/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.IdProveedor)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedores/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            
            if (proveedor == null) return NotFound();

            return View(proveedor);
        }

        // POST: Proveedores/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Proveedores/Reporte
        public async Task<IActionResult> Reporte()
        {
            // Buscamos el primer proveedor para mostrarlo en las tarjetas del reporte
            var proveedorLider = await _context.Proveedores.FirstOrDefaultAsync();

            ViewBag.TotalProveedores = await _context.Proveedores.CountAsync();
            ViewBag.TotalProductos = await _context.Productos.CountAsync();

            return View(proveedorLider); // Le enviamos el modelo a la vista Reporte.cshtml
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }
}