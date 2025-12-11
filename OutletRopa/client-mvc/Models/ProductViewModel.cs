namespace client_mvc.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal PrecioOriginal { get; set; }
        public string Talla { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;

        public string TipoProducto { get; set; }
        public int? TemporadaId { get; set; }
        public decimal DescuentoPorcentaje { get; set; } = 0;
    }
}