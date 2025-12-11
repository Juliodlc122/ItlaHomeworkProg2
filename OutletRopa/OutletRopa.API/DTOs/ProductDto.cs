namespace OutletRopa.API.DTOs
{
    
    public class ProductDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public decimal PrecioOriginal { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; }
        public string Talla { get; set; }

        public string Material { get; set; }
        public string Tipo { get; set; }

       
        public int? TemporadaId { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }

        public string Color { get; set; }
    }
}