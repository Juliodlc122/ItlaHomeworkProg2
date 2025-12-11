using System;
using System.Collections.Generic;

namespace client_mvc.Models
{
    public class VentaViewModel
    {
       
        public string CodigoPedido { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal TotalPagado { get; set; }
        public int ClienteId { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<string> ResumenProductos { get; set; } = new List<string>();
        public int Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}