
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace client_mvc.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccesoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = _httpClientFactory.CreateClient("OutletApi");

            var loginData = new { Email = email, Password = password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var usuario = JsonSerializer.Deserialize<UsuarioSesion>(jsonString, opciones);
                if (usuario == null)
                {
                    ViewBag.Error = "Respuesta inválida del servidor.";
                    return View();
                }

                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre ?? string.Empty);
                HttpContext.Session.SetString("UsuarioRol", usuario.Rol ?? string.Empty);
                HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());

                if (usuario.Rol == "Empleado")
                    return RedirectToAction("ReporteVentas", "Home");

                return RedirectToAction("Index", "Tienda");
            }

            // Intentar leer mensaje de error devuelto por la API
            var errorBody = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(errorBody))
            {
                ViewBag.Error = errorBody;
            }
            else
            {
                ViewBag.Error = "Email o contraseña incorrectos";
            }

            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(string nombre, string email, string password, string direccion, string telefono)
        {
            var client = _httpClientFactory.CreateClient("OutletApi");

            var registroData = new
            {
                Nombre = nombre,
                Email = email,
                Password = password,
                Direccion = direccion,
                Telefono = telefono
            };

            var content = new StringContent(JsonSerializer.Serialize(registroData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Auth/registrar", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["MensajeExito"] = "¡Cuenta creada! Ahora inicia sesión.";
                return RedirectToAction("Login");
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(errorBody))
            {
                ViewBag.Error = errorBody;
            }
            else
            {
                ViewBag.Error = "No se pudo registrar. Verifique los datos e intente de nuevo.";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

    public class UsuarioSesion
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Rol { get; set; }
    }
}