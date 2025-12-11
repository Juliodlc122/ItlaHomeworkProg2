using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OutletRopa.Application.DTOs
{
    public class CrearProductoDto
    {
        public int Id { get; set; } 

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public decimal PrecioOriginal { get; set; }

        [Required]
        public string Talla { get; set; } = string.Empty;

        [Required]
        public int Stock { get; set; } 
        public int? TemporadaId { get; set; } 

        public decimal DescuentoPorcentaje { get; set; } = 0;

        [Required]
        public string TipoProducto { get; set; } = string.Empty; 

        public string DetalleEspecifico { get; set; } = string.Empty;

        public string? ImagenBase64 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ImagenUrl { get; set; }
    }
}