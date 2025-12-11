using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRopa.Domain.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; } = null!;

        public int CamisaId { get; set; }
        public virtual Camisa Camisa { get; set; } = null!;

        public decimal TotalPagado { get; set; }

        public Venta() { Fecha = DateTime.Now; } 

        public Venta(int clienteId, int camisaId, decimal total)
        {
            Fecha = DateTime.Now;
            ClienteId = clienteId;
            CamisaId = camisaId;
            TotalPagado = total;
        }
    }
}