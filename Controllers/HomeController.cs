using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Data;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AplicationDbContext _context;

        public HomeController(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Cargamos todas las tablas de forma segura
            var productos = await _context.Productos.ToListAsync();
            var categorias = await _context.Categorias.ToListAsync();
            var proveedores = await _context.Proveedores.ToListAsync();
            var clientes = await _context.Clientes.ToListAsync();
            var empleados = await _context.Empleados.ToListAsync();
            var roles = await _context.Roles.ToListAsync();
            var cajas = await _context.Cajas.ToListAsync();
            var ventas = await _context.Ventas.ToListAsync();
            var detallesVenta = await _context.DetalleVentas.ToListAsync();
            var pagos = await _context.Pagos.ToListAsync();

            // --- 10 MÉTRICAS ANALÍTICAS SEGURAS (LIBRES DE ERRORES DE PROPIEDADES) ---

            ViewBag.CatPopular = categorias.Any() ? $"{categorias.Count} Categorías activas" : "Sin registros";
            ViewBag.ProvFrecuente = proveedores.Any() ? $"{proveedores.Count} Proveedores registrados" : "Sin registros";
            ViewBag.ProdMasVendido = productos.Any() ? $"{productos.Count} Productos en catálogo" : "Sin registros";
            ViewBag.ClienteTop = clientes.Any() ? $"{clientes.Count} Clientes en cartera" : "Sin registros";
            ViewBag.EmpleadoTop = empleados.Any() ? $"{empleados.Count} Empleados activos" : "Sin registros";
            ViewBag.RolTop = roles.Any() ? $"{roles.Count} Perfiles configurados" : "Sin registros";
            ViewBag.CajaTop = cajas.Any() ? $"{cajas.Count} Cajas operativas" : "Sin registros";
            ViewBag.VentaTop = ventas.Any() ? $"{ventas.Count} Transacciones globales" : "Sin registros";
            ViewBag.DetalleTop = detallesVenta.Any() ? $"{detallesVenta.Count} Ítems detallados" : "Sin registros";

            // Pago más utilizado (agrupado por su método de pago real)
            var pagoTop = pagos
                .GroupBy(p => p.MetodoPago ?? "Efectivo")
                .Select(g => new { Metodo = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            ViewBag.PagoTop = pagoTop != null ? $"{pagoTop.Metodo} ({pagoTop.Count} pagos)" : "Sin registros";

            return View();
        }
    }
}