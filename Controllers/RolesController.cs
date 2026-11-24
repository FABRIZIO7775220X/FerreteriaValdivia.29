using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class RolesController : Controller
    {
        private readonly AplicationDbContext _context;

        public RolesController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles);
        }

        // GET: Roles/Reporte (Analítica de Roles)
        public async Task<IActionResult> Reporte()
        {
            var roles = await _context.Roles.ToListAsync();
            return View(roles); // Envía la lista completa para que el reporte calcule el total correctamente
        }

        // GET: Roles/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Roles/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null) return NotFound();

            return View("Editar", rol); // <-- Apunta explícitamente a Editar.cshtml
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol rol)
        {
            if (id != rol.id_Rol) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Roles.Any(e => e.id_Rol == rol.id_Rol)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Editar", rol); // <-- Apunta explícitamente a Editar.cshtml si hay error de validación
        }

        // GET: Roles/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.id_Rol == id);

            if (rol == null) return NotFound();

            return View(rol);
        }

        // POST: Roles/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id_Rol)
        {
            var rol = await _context.Roles.FindAsync(id_Rol);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}