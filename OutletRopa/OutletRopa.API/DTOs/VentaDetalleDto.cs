namespace OutletRopa.API.DTOs
{
    public class VentaDetalleDto
    {
        public int Id { get; set; }
        public int CamisaId { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public string CuponDescuento { get; set; }
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Precio { get; set; }

        public string NombreProducto { get; set; }
        public string ImagenUrl { get; set; }
        
    }
}