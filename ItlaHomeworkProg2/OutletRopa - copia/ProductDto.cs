
namespace OutletRopa.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        
        public decimal PrecioOriginal { get; set; }

        
        public decimal Precio
        {
            get => PrecioOriginal;
            set => PrecioOriginal = value;
        }

        public string Talla { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Stock { get; set; }
        public bool? EsMangaLarga { get; set; }
        public bool? EsJeans { get; set; }
        public string? Material { get; set; }
    }
}