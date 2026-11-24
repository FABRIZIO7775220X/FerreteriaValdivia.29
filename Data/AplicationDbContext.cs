using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        // Definición única y limpia de los DbSet en plural
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo exacto de los nombres de las tablas en la base de datos SQLite
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Proveedor>().ToTable("Proveedor");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Caja>().ToTable("Caja");
            modelBuilder.Entity<Venta>().ToTable("Venta");
            modelBuilder.Entity<DetalleVenta>().ToTable("DetalleVenta");
            modelBuilder.Entity<Pago>().ToTable("Pago");

            // Configuración específica para Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(e => e.CodigoProducto);
                entity.Property(e => e.CodigoProducto).HasColumnName("CodigoProducto");
                entity.Property(e => e.Nombre).HasColumnName("Nombre_Producto");
                entity.Property(e => e.PrecioVenta).HasColumnName("PrecioVenta");
                entity.Property(e => e.StockMinimo).HasColumnName("StockMinimo");
                entity.Property(e => e.StockActual).HasColumnName("StockActual");
                entity.Property(e => e.CantidadVendida).HasColumnName("CantidadVendida");
                entity.Property(e => e.IdCategoria).HasColumnName("id_Categoria");
                entity.Property(e => e.IdProveedor).HasColumnName("id_Proveedor");
            });
        }
    }
}