using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Venta")]
    public class Venta
    {
        [Key]
        [Column("id_Venta")]
        public int id_Venta { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Column("Hora")]
        public TimeSpan Hora { get; set; } = DateTime.Now.TimeOfDay;

        [Required]
        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Column("id_Cliente")]
        public int id_Cliente { get; set; }

        [ForeignKey("id_Cliente")]
        public Cliente? Cliente { get; set; }

        [Column("id_Empleado")]
        public int id_Empleado { get; set; }

        [ForeignKey("id_Empleado")]
        public Empleado? Empleado { get; set; }

        [Column("id_Caja")]
        public int id_Caja { get; set; }

        [ForeignKey("id_Caja")]
        public Caja? Caja { get; set; }
    }
}