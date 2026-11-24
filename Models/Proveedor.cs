using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        [Column("id_Proveedor")]
        public int IdProveedor { get; set; } // <-- Cambiado a IdProveedor para coincidir con el controlador y las vistas

        [Required]
        [StringLength(100)]
        [Column("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("Telefono")]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("Ubicacion")]
        public string Ubicacion { get; set; } = string.Empty;
    }
}