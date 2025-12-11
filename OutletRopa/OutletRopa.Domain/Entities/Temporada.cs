using System;
using System.Collections.Generic;

namespace OutletRopa.Domain.Entities
{
    public class Temporada
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }

        public ICollection<Producto> Prendas { get; set; } = new List<Producto>();
    }
}
