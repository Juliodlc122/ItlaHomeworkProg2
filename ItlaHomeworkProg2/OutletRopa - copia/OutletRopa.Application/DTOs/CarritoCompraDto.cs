using System.Collections.Generic;

namespace OutletRopa.Application.DTOs;
    public class CarritoCompraDto
    {
        public int ClienteId { get; set; }
        public string MetodoPago { get; set; }
        public string CuponDescuento { get; set; }
        public List<ProductoCompraDto> Productos { get; set; }
    }

    public class ProductoCompraDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
    }

    public class PedidoResumenDto
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
    }
}