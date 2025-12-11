using Microsoft.AspNetCore.Http;

namespace OutletRopa.API.DTOs
{
    public class InventarioProductoDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal PrecioOriginal { get; set; }
        public string Talla { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string Tipo { get; set; } = "Camisa";
        public string Material { get; set; } = string.Empty;
        public int? TemporadaId { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }

        public string? ImagenBase64 { get; set; }
        public string? FileName { get; set; }
    }
}
