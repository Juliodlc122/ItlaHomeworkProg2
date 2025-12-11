using Microsoft.AspNetCore.Mvc;
using client_mvc.Models; 
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace client_mvc.Controllers
{
    public class TiendaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string SessionKeyCart = "MiCarrito";

        public TiendaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<IActionResult> Index(string categoria = null)
        {
            var client = _httpClientFactory.CreateClient("OutletApi");
            string endpoint = string.IsNullOrEmpty(categoria) ? "api/Ropa/obtener-todas" : GetEndpointForCategory(categoria);

            try
            {
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode) return View(new List<ProductViewModel>());

                var jsonString = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var productos = JsonSerializer.Deserialize<List<ProductViewModel>>(jsonString, opciones) ?? new List<ProductViewModel>();

                NormalizePrices(productos);
                return View(productos);
            }
            catch
            {
                return View(new List<ProductViewModel>());
            }
        }

        private string GetEndpointForCategory(string cat)
        {
            return cat.ToLower() switch
            {
                "camisa" or "camisas" => "api/Ropa/camisas",
                "pantalon" or "pantalones" => "api/Ropa/pantalones",
                "zapato" or "zapatos" => "api/Ropa/zapatos",
                _ => "api/Ropa/obtener-todas"
            };
        }

        public async Task<IActionResult> Camisas() => await Index("Camisa");
        public async Task<IActionResult> Pantalones() => await Index("Pantalon");
        public async Task<IActionResult> Zapatos() => await Index("Zapato");

      
        public async Task<IActionResult> AgregarAlCarrito(int id)
        {
            var client = _httpClientFactory.CreateClient("OutletApi");
            var response = await client.GetAsync("api/Ropa/obtener-todas");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var todas = JsonSerializer.Deserialize<List<ProductViewModel>>(json, opciones);
                var producto = todas?.FirstOrDefault(c => c.Id == id);

                if (producto != null)
                {
                    var carrito = ObtenerCarritoDeSession();
                    carrito.Add(producto);
                    GuardarCarritoEnSession(carrito);
                    TempData["MensajeExito"] = "Agregado al carrito";
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Carrito() => View(ObtenerCarritoDeSession());

        public IActionResult EliminarDelCarrito(int id)
        {
            var carrito = ObtenerCarritoDeSession();
            var item = carrito.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                carrito.Remove(item);
                GuardarCarritoEnSession(carrito);
            }
            return RedirectToAction("Carrito");
        }

        [HttpGet]
        public IActionResult IrAPagar(string codigoCupon)
        {
            var usuarioId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(usuarioId))
                return RedirectToAction("Login", "Acceso", new { returnUrl = Url.Action("IrAPagar", "Tienda", new { codigoCupon }) });

            var carrito = ObtenerCarritoDeSession();
            if (carrito == null || !carrito.Any()) return RedirectToAction("Index");

            ViewBag.Total = carrito.Sum(x => x.Precio > 0 ? x.Precio : x.PrecioOriginal);
            TempData["CuponUsado"] = codigoCupon;
            return View("Pago");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcesarPagoReal([FromForm] CrearVentaDto model)
        {
            int clienteId = await ObtenerClienteId();
            if (clienteId == 0) return RedirectToAction("Login", "Acceso");

            var carrito = ObtenerCarritoDeSession();
            if (carrito == null || !carrito.Any()) return RedirectToAction("Carrito");

            var ordenCompra = new CarritoCompraDto
            {
                ClienteId = clienteId,
                MetodoPago = model.MetodoPago ?? "Tarjeta",
                CuponDescuento = TempData["CuponUsado"] as string ?? "OUTLET2025",
                Productos = carrito.Select(p => new ProductoCompraDto
                {
                    Id = p.Id,
                    Tipo = !string.IsNullOrEmpty(p.TipoProducto) ? p.TipoProducto : "Camisa"
                }).ToList()
            };

            var client = _httpClientFactory.CreateClient("OutletApi");
            var token = HttpContext.Session.GetString("UsuarioToken");
            if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("api/Ventas/procesar-carrito", ordenCompra);

            if (!response.IsSuccessStatusCode)
            {
                TempData["ApiError"] = "Error al procesar el pago.";
                return RedirectToAction("Carrito");
            }

            HttpContext.Session.Remove(SessionKeyCart);
            return View("CompraExitosa");
        }

        public IActionResult CompraExitosa() => View();

        
        [HttpGet]
        public async Task<IActionResult> MisCompras()
        {
            int clienteId = await ObtenerClienteId();
            if (clienteId == 0) return RedirectToAction("Login", "Acceso");

            var client = _httpClientFactory.CreateClient("OutletApi");

           
            var pedidos = new List<client_mvc.Models.PedidoResumenDto>();

            try
            {
                var response = await client.GetAsync($"api/Ventas/por-cliente/{clienteId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                   
                    pedidos = JsonSerializer.Deserialize<List<client_mvc.Models.PedidoResumenDto>>(json, opciones) ?? new();
                }
            }
            catch { }

            return View(pedidos);
        }

        [HttpGet]
        public async Task<IActionResult> ReporteVentas()
        {
            if (HttpContext.Session.GetString("UsuarioRol") != "Empleado")
                return RedirectToAction("Index", "Home");

            var client = _httpClientFactory.CreateClient("OutletApi");

           
            var pedidos = new List<client_mvc.Models.PedidoResumenDto>();

            try
            {
                var response = await client.GetAsync("api/Ventas");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    
                    pedidos = JsonSerializer.Deserialize<List<client_mvc.Models.PedidoResumenDto>>(json, opciones) ?? new();
                }
            }
            catch { }

            return View(pedidos);
        }

        private async Task<int> ObtenerClienteId()
        {
            var cId = HttpContext.Session.GetString("ClienteId");
            if (int.TryParse(cId, out int id)) return id;
            var uId = HttpContext.Session.GetString("UsuarioId");
            if (string.IsNullOrEmpty(uId)) return 0;

            try
            {
                var client = _httpClientFactory.CreateClient("OutletApi");
                var resp = await client.GetAsync($"api/Clientes/por-usuario/{uId}");
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("id", out var idProp))
                    {
                        HttpContext.Session.SetString("ClienteId", idProp.GetInt32().ToString());
                        return idProp.GetInt32();
                    }
                }
            }
            catch { }
            return 0;
        }

        private List<ProductViewModel> ObtenerCarritoDeSession()
        {
            var s = HttpContext.Session.GetString(SessionKeyCart);
            if (string.IsNullOrEmpty(s)) return new List<ProductViewModel>();
            return JsonSerializer.Deserialize<List<ProductViewModel>>(s) ?? new List<ProductViewModel>();
        }

        private void GuardarCarritoEnSession(List<ProductViewModel> c)
        {
            HttpContext.Session.SetString(SessionKeyCart, JsonSerializer.Serialize(c));
        }

        private void NormalizePrices(IEnumerable<ProductViewModel> list)
        {
            if (list == null) return;
            foreach (var p in list) if (p.PrecioOriginal == 0) p.PrecioOriginal = p.Precio;
        }
    }
}
