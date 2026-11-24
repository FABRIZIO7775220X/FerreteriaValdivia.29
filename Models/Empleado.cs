using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        [Key]
        [Column("id_Empleado")]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(80)]
        [Column("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("Cargo")]
        public string Cargo { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("Telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("id_Rol")]
        public int? IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }
    }
}