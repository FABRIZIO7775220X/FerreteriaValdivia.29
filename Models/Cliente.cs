using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("id_Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(80)]
        [Column("Nombre")] // O "nombre" según tu BD
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("Telefono")] // O "telefono" según tu BD
        public string Telefono { get; set; } = string.Empty;

        [StringLength(200)]
        [Column("Direccion")] // O "direccion" según tu BD
        public string Direccion { get; set; } = string.Empty;
    }
}