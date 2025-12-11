using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutletRopa.Domain.Entities;
using OutletRopa.Persistence.Contexts;
using System.Threading.Tasks;

namespace OutletRopa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<IActionResult> GetClientePorUsuario(int usuarioId)
        {
            
            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (usuario == null) return NotFound("Usuario no encontrado");

           
            var cliente = await _context.Clientes
                                        .FirstOrDefaultAsync(c => c.Email == usuario.Email);

            if (cliente == null)
            {
                cliente = new Cliente
                {
                    Nombre = usuario.Nombre,
                    Email = usuario.Email
                };
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }

            return Ok(cliente);
        }
    }
}