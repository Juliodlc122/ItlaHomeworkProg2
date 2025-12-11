public class PedidoResumenDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }

    public int CodigoPedido => Id;
    public decimal TotalPagado => Total; 
    public string Estado { get; set; } = "Completado"; 
    public string ResumenProductos { get; set; } = "Ver detalles"; 
    public int ClienteId { get; set; } = 0; 