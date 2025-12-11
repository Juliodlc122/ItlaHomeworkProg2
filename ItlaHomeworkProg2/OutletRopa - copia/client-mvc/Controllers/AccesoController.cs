using client_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using OutletRopa.Application.DTOs;

namespace client_mvc.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccesoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto modelo, string returnUrl = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OutletApi");
                var json = JsonSerializer.Serialize(modelo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                  
                    using var doc = JsonDocument.Parse(responseString);
                    var root = doc.RootElement;

                    int usuarioId = root.GetProperty("id").GetInt32();
                    string nombre = root.GetProperty("nombre").GetString() ?? string.Empty;
                    string rol = root.GetProperty("rol").GetString() ?? string.Empty;

                    HttpContext.Session.SetString("UsuarioId", usuarioId.ToString());
                    HttpContext.Session.SetString("UsuarioNombre", nombre);
                    HttpContext.Session.SetString("UsuarioRol", rol);

                    if (root.TryGetProperty("clienteId", out var clienteProp) && clienteProp.ValueKind != JsonValueKind.Null)
                    {
                        HttpContext.Session.SetString("ClienteId", clienteProp.GetInt32().ToString());
                    }

                   
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    if (rol == "Empleado")
                        return RedirectToAction("ReporteVentas", "Home");

                    return RedirectToAction("Index", "Tienda");
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Error API ({response.StatusCode}): {err}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error de conexión: " + ex.Message;
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(modelo);
        }
        public IActionResult Registrar() => View();

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroDto modelo)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OutletApi");
                var json = JsonSerializer.Serialize(modelo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Auth/registro", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["MensajeExito"] = "¡Cuenta creada con éxito! Ahora inicia sesión.";
                    return RedirectToAction("Login");
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"No se pudo registrar: {errorMsg}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error crítico: " + ex.Message;
            }

            return View(modelo);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

  
    public class UsuarioSesion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
    }
}