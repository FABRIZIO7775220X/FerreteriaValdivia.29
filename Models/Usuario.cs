using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FerreteriaValdivia.Models
{
    [Table("Usuario")] // O el nombre exacto de tu tabla en MariaDB (ej. "Usuarios")
    public class Usuario
    {
        [Key]
        [Column("id_Usuario")] // Ajusta si en tu BD es "IdUsuario" o "id_usuario"
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Nombre_Completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("Username")]
        public string Username { get; set; } = string.Empty;

        [Column("Activo")]
        public bool Activo { get; set; }

        [Required]
        [StringLength(255)]
        [Column("Password_Hash")]
        public string PasswordHash { get; set; } = string.Empty;

        // Constructor vacío (obligatorio para Entity Framework Core)
        public Usuario()
        {
        }

        // Constructor con parámetros
        public Usuario(int idUsuario, string nombreCompleto, string username, string passwordHash, bool activo)
        {
            IdUsuario = idUsuario;
            NombreCompleto = nombreCompleto;
            Username = username;
            PasswordHash = passwordHash;
            Activo = activo;
        }
    }
}