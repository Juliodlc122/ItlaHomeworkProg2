using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RopaOutlet.Domain
{
    public class Prenda
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioDescuento { get; set; }
        public bool EnOferta { get; set; } 
        public int Stock { get; set; }

        
        public decimal PrecioFinal => EnOferta ? PrecioDescuento : PrecioOriginal;
    }
}