namespace OutletRopa.Application.DTOs
{
    public class DashboardDto
    {
        public decimal TotalIngresos { get; set; }
        public int CantidadVentas { get; set; }
        public int CantidadProductos { get; set; }
        public int CantidadClientes { get; set; }

        public List<VentaDiariaDto> VentasPorFecha { get; set; } = new List<VentaDiariaDto>();
    }
}