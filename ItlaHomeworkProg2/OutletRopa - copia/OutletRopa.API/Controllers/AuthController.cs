using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutletRopa.Domain.Entities;
using OutletRopa.Persistence.Contexts;
using System.Threading.Tasks;

namespace OutletRopa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if (usuario == null) return BadRequest("Datos inválidos.");

            var existe = await _context.Usuarios
                .AnyAsync(u => u.Email == usuario.Email);

            if (existe)
                return BadRequest("El correo ya está registrado.");

            if (string.IsNullOrEmpty(usuario.Rol))
                usuario.Rol = "Cliente";

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario registrado exitosamente", usuarioId = usuario.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest("Faltan datos de acceso.");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);

            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });

            return Ok(usuario);
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}