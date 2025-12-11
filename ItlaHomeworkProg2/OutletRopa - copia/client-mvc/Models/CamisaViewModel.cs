using System.Text.Json.Serialization;

namespace client_mvc.Models
{
    public class CamisaViewModel
    {
        public int Id { get; set; }

      public string Name { get; set; }
        public string Nombre { get; set; } = string.Empty;
        
        [JsonPropertyName("precio")]
        public decimal PrecioOriginal { get; set; }

        public decimal Precio { get; set; }
        public string Talla { get; set; } = string.Empty;

        public bool EsMangaLarga { get; set; }

        [JsonPropertyName("imagenUrl")]
        public string ImagenUrl { get; set; } = string.Empty;
    }
}