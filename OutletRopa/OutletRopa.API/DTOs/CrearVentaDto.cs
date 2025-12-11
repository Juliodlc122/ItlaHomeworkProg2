using System.ComponentModel.DataAnnotations;

namespace OutletRopa.API.DTOs
{
    public class CrearVentaDto
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int CamisaId { get; set; }

       [Required]
        public string? CuponDescuento { get; set; }
        public string MetodoPago { get; set; }

    }
}