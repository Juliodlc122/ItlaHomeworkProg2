using System.ComponentModel.DataAnnotations;

namespace OutletRopa.API.DTOs
{
    public class CrearClienteDto
    {
        [Required]
        public string NombreCompleto { get; set; } = string.Empty; 

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}