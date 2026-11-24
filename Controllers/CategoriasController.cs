using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly AplicationDbContext _context;

        public CategoriasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias/Index
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return View(categorias);
        }

        // GET: Categorias/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Categorias/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("IdCategoria,NombreCategoria")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("IdCategoria,NombreCategoria")] Categoria categoria)
        {
            if (id != categoria.IdCategoria) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.IdCategoria)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Eliminar/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null) return NotFound();

            var categoria = await _context.Categorias.FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categoria == null) return NotFound();

            return View(categoria);
        }

        // POST: Categorias/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Categorias/Reporte
        public async Task<IActionResult> Reporte()
        {
            ViewBag.TotalCategorias = await _context.Categorias.CountAsync();
            ViewBag.TotalProductos = await _context.Productos.CountAsync();
            return View();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id);
        }
    }
}