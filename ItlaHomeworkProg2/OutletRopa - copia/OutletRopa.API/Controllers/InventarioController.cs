using Microsoft.AspNetCore.Mvc;
using OutletRopa.Domain.Entities;
using OutletRopa.Domain.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace OutletRopa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerInventario()
        {
            var productos = await _unitOfWork.Repository<Producto>().GetAllAsync();
            return Ok(productos);
        }

        [HttpPost("actualizar-stock/{id}")]
        public async Task<IActionResult> ActualizarStock(int id, [FromBody] int cantidad)
        {
            var repo = _unitOfWork.Repository<Producto>();
            var producto = await repo.GetByIdAsync(id);

            if (producto == null) return NotFound();

            producto.Stock = cantidad;
            await repo.UpdateAsync(producto);

            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var repo = _unitOfWork.Repository<Producto>();
            var producto = await repo.GetByIdAsync(id);

            if (producto == null) return NotFound();

            await repo.DeleteAsync(producto);
            return NoContent();
        }

        [HttpGet("valor-total")]
        public async Task<IActionResult> ObtenerValorTotal()
        {
            var productos = await _unitOfWork.Repository<Producto>().GetAllAsync();
            decimal total = 0;

            foreach (var p in productos)
            {
                total += p.Precio * p.Stock;
            }

            return Ok(total);
        }
    }
}