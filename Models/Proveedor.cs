using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        [Column("id_Proveedor")]
        public int IdProveedor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(200)]
        public string Ubicacion { get; set; } = string.Empty;
    }
}