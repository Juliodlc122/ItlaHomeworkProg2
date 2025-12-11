using Microsoft.AspNetCore.Mvc;
using OutletRopa.Domain.Entities;
using OutletRopa.Domain.Interfaces;
using OutletRopa.Application.DTOs; 
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OutletRopa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VentasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("procesar-carrito")]
        public async Task<IActionResult> ProcesarCarrito([FromBody] CarritoCompraDto orden)
        {
            if (orden == null || orden.Productos == null || orden.Productos.Count == 0)
                return BadRequest("El carrito está vacío.");

            decimal totalVenta = 0;
            var repoProductos = _unitOfWork.Repository<Producto>();

            
            foreach (var item in orden.Productos)
            {
                var producto = await repoProductos.GetByIdAsync(item.Id);

                if (producto != null)
                {
                    totalVenta += producto.CalcularPrecioFinal();

                    if (producto.Stock > 0)
                    {
                        producto.Stock--;
                        await repoProductos.UpdateAsync(producto);
                    }
                }
            }

            var nuevaVenta = new Venta
            {
                Fecha = DateTime.Now,
                Total = totalVenta,
              
            };

            await _unitOfWork.Repository<Venta>().AddAsync(nuevaVenta);

            return Ok(new { mensaje = "Compra procesada exitosamente", ventaId = nuevaVenta.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetVentas()
        {
            return Ok(await _unitOfWork.Repository<Venta>().GetAllAsync());
        }

        [HttpGet("por-cliente/{clienteId}")]
        public async Task<IActionResult> GetVentasPorCliente(int clienteId)
        {
            var ventas = await _unitOfWork.Repository<Venta>().GetAllAsync();
            return Ok(ventas);
        }
    }
}