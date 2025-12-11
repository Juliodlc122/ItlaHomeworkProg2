using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RopaOutlet.Domain
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int PrendaId { get; set; }
        public int Cantidad { get; set; }
        public decimal TotalPagado { get; set; }
    }
}