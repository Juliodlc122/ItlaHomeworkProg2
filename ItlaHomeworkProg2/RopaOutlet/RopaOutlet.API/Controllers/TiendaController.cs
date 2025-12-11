using Microsoft.AspNetCore.Mvc;
using RopaOutlet.Application.Interfaces;
using RopaOutlet.Domain;

namespace RopaOutlet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        private readonly IOutletRepository _repo;

        public TiendaController(IOutletRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Prenda>>> Get()
        {
            return Ok(await _repo.GetPrendasAsync());
        }

        [HttpPost("comprar")]
        public async Task<IActionResult> Comprar([FromBody] int id)
        {
            bool exito = await _repo.ComprarAsync(id, 1); // Compra 1 unidad
            if (exito) return Ok("Compra exitosa");
            return BadRequest("Sin stock o producto no encontrado");
        }
    }
}