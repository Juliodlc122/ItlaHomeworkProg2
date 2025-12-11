using Microsoft.AspNetCore.Mvc;
using OutletRopa.Domain.Entities;
using OutletRopa.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace OutletRopa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RopaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RopaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]             
        [HttpGet("todos")]    
        [HttpGet("obtener-todas")] 
        public async Task<IActionResult> Get()
        {
            var productos = await _unitOfWork.Repository<Producto>().GetAllAsync();
            return Ok(productos);
        }

        
        [HttpGet("camisa")]
        [HttpGet("camisas")]
        public async Task<IActionResult> GetCamisas()
        {
            var camisas = await _unitOfWork.Repository<Camisa>().GetAllAsync();
            return Ok(camisas);
        }

       
        [HttpGet("pantalon")]
        [HttpGet("pantalones")]
        public async Task<IActionResult> GetPantalones()
        {
            var pantalones = await _unitOfWork.Repository<Pantalon>().GetAllAsync();
            return Ok(pantalones);
        }

       
        [HttpGet("zapato")]
        [HttpGet("zapatos")]
        public async Task<IActionResult> GetZapatos()
        {
            var zapatos = await _unitOfWork.Repository<Zapato>().GetAllAsync();
            return Ok(zapatos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _unitOfWork.Repository<Producto>().GetByIdAsync(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }


        [HttpPost("camisa")]
        public async Task<IActionResult> CreateCamisa(Camisa camisa)
        {
            camisa.TipoProducto = "Camisa";
            await _unitOfWork.Repository<Camisa>().AddAsync(camisa);
            return Ok(camisa);
        }

        [HttpPost("pantalon")]
        public async Task<IActionResult> CreatePantalon(Pantalon pantalon)
        {
            pantalon.TipoProducto = "Pantalon";
            await _unitOfWork.Repository<Pantalon>().AddAsync(pantalon);
            return Ok(pantalon);
        }

        [HttpPost("zapato")]
        public async Task<IActionResult> CreateZapato(Zapato zapato)
        {
            zapato.TipoProducto = "Zapato";
            await _unitOfWork.Repository<Zapato>().AddAsync(zapato);
            return Ok(zapato);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var repo = _unitOfWork.Repository<Producto>();
            var producto = await repo.GetByIdAsync(id);

            if (producto == null) return NotFound();

            await repo.DeleteAsync(producto);
            return NoContent();
        }

        [HttpGet("{id}/precio-final")]
        public async Task<IActionResult> GetPrecioFinal(int id)
        {
            var producto = await _unitOfWork.Repository<Producto>().GetByIdAsync(id);
            if (producto == null) return NotFound();

            return Ok(producto.CalcularPrecioFinal());
        }
    }
}