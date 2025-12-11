using System;
using System.Collections.Generic;

namespace client_mvc.Models
{
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
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }



        public int CodigoPedido => Id;           
        public decimal TotalPagado => Total;     

       
        public string Estado { get; set; } = "Completado";
        public List<string> ResumenProductos { get; set; } = new List<string> { "Ver detalles en factura" };
        public int ClienteId { get; set; } = 0;
    }
}