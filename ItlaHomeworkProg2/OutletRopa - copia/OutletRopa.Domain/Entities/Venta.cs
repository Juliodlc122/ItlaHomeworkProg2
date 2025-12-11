using System;

namespace OutletRopa.Domain.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int? CamisaId { get; set; }
        public int? PantalonId { get; set; }
        public int? ZapatoId { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string CodigoPedido { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";

        public Venta() { }

      
        public Venta(int clienteId, int productoId, decimal total, string metodoPago)
        {
            ClienteId = clienteId;
            CamisaId = productoId; 
            Total = total;
            MetodoPago = metodoPago;
            Fecha = DateTime.Now;
        }

        public Venta(int clienteId, int productoId, decimal total, string metodoPago, string codigoPedido)
            : this(clienteId, productoId, total, metodoPago)
        {
            CodigoPedido = codigoPedido;
        }

        public Venta(int clienteId, int productoId, decimal total, string metodoPago, string codigoPedido, string tipoProducto)
            : this(clienteId, productoId, total, metodoPago, codigoPedido)
        {
            if (string.IsNullOrWhiteSpace(tipoProducto)) return;

            switch (tipoProducto.Trim().ToLowerInvariant())
            {
                case "pantalon":
                case "pantalón":
                    PantalonId = productoId;
                    CamisaId = null;
                    break;
                case "zapato":
                    ZapatoId = productoId;
                    CamisaId = null;
                    break;
                default:
                    CamisaId = productoId;
                    break;
            }
        }
    }
}