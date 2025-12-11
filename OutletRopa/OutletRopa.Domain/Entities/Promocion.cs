using System;

namespace OutletRopa.Domain.Entities
{
    public class Promocion
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public decimal DescuentoPorcentaje { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activa { get; set; } = true;
    }
}
