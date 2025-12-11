using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OutletRopa.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }


        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
        public string Rol { get; set; } = "Cliente";
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public Usuario() { }

        
        public Usuario(string nombre, string email, string password, string rol)
        {
            Nombre = nombre;
            Email = email;
            Password = password;
            Rol = rol;
        }
    }
}