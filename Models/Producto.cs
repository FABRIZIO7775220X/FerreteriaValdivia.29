using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        [StringLength(50)]
        [Column("CodigoProducto")]
        public string CodigoProducto { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("Nombre_Producto")]
        public string Nombre { get; set; } = string.Empty;

        [Column("PrecioVenta", TypeName = "decimal(18,2)")]
        public decimal PrecioVenta { get; set; }

        [Column("StockMinimo")]
        public int StockMinimo { get; set; }

        [Column("StockActual")]
        public int StockActual { get; set; }

        [Column("CantidadVendida")]
        public int CantidadVendida { get; set; }

        // Llaves foráneas opcionales para las relaciones del diagrama
        [Column("id_Categoria")]
        public int? IdCategoria { get; set; }

        [Column("id_Proveedor")]
        public int? IdProveedor { get; set; }
    }
}