using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("DetalleVenta")]
    public class DetalleVenta
    {
        [Key]
        [Column("id_DetalleVenta")]
        public int id_DetalleVenta { get; set; }

        [Required]
        [Column("id_Venta")]
        public int id_Venta { get; set; }

        [ForeignKey("id_Venta")]
        public Venta? Venta { get; set; }

        [Required]
        [Column("id_Producto")]
        public string id_Producto { get; set; } = string.Empty;

        [ForeignKey("id_Producto")]
        public Producto? Producto { get; set; }

        [Required]
        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("Precio_Unitario", TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Column("Subtotal", TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }
    }
}