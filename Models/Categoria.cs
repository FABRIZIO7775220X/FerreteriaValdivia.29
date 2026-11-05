using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Column("id_Categoria")]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Nombre_categoria")]
        public string NombreCategoria { get; set; } = string.Empty;
    }
}