using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Rol")]
    public class Rol
    {
        [Key]
        public int id_Rol { get; set; }

        [Required]
        [Column("Nombre_Rol")] // Mapea exactamente con el nombre de la columna en tu SQL
        public string NombreRol { get; set; } = string.Empty;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}