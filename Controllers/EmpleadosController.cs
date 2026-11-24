using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly AplicationDbContext _context;

        public EmpleadosController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }

        // GET: Empleados/Crear
        public IActionResult Crear()
        {
            return View("Crear");
        }

        // POST: Empleados/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Crear", empleado);
        }

        // GET: Empleados/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            return View("Editar", empleado);
        }

        // POST: Empleados/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Empleado empleado)
        {
            if (id != empleado.IdEmpleado) 
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Empleados.Any(e => e.IdEmpleado == empleado.IdEmpleado))
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
            return View("Editar", empleado);
        }

        // GET: Empleados/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var empleado = await _context.Empleados.FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null) return NotFound();

            return View("Eliminar", empleado);
        }

        // POST: Empleados/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleados/Reporte
        public async Task<IActionResult> Reporte()
        {
            ViewBag.TotalEmpleados = await _context.Empleados.CountAsync();
            ViewBag.TotalRoles = await _context.Roles.CountAsync();
            return View();
        }
    }
}