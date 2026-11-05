using System;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaValdivia.Models
{
    public class Usuario
    {
        [Key] // <-- LE INDICA A EF CORE QUE ESTA ES LA LLAVE PRIMARIA
        public int IdUsuario { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool Activo { get; set; }
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