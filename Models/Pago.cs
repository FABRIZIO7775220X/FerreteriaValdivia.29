using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Pago")]
    public class Pago
    {
        [Key]
        [Column("id_Pago")]
        public int id_Pago { get; set; }

        [Required]
        [Column("Monto", TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required]
        [Column("MetodoPago")]
        public string MetodoPago { get; set; } = string.Empty;

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("Hora")]
        public TimeSpan Hora { get; set; }

        [Required]
        [Column("id_Venta")]
        public int id_Venta { get; set; }

        [ForeignKey("id_Venta")]
        public Venta? Venta { get; set; }
    }
}