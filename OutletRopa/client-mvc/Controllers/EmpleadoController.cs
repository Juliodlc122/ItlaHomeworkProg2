using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using client_mvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace client_mvc.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmpleadoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private bool EsEmpleado()
        {
            var rol = HttpContext.Session.GetString("UsuarioRol");
            return string.Equals(rol, "Empleado", StringComparison.OrdinalIgnoreCase);
        }

        public async Task<IActionResult> Index()
        {
            if (!EsEmpleado()) return RedirectToAction("Index", "Home");

            var client = _httpClientFactory.CreateClient("OutletApi");
            try
            {
                var resp = await client.GetAsync("api/Inventario");
                if (!resp.IsSuccessStatusCode)
                {
                    TempData["ApiError"] = await resp.Content.ReadAsStringAsync();
                    return View(new List<ProductViewModel>());
                }

                var json = await resp.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var productos = JsonSerializer.Deserialize<List<ProductViewModel>>(json, opciones) ?? new List<ProductViewModel>();
                return View(productos);
            }
            catch (Exception ex)
            {
                TempData["ApiError"] = ex.Message;
                return View(new List<ProductViewModel>());
            }
        }

        public IActionResult Crear()
        {
            if (!EsEmpleado()) return RedirectToAction("Index", "Home");
            return View(new InventarioProductoFormModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(InventarioProductoFormModel model)
        {
            if (!EsEmpleado()) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid) return View(model);

            var client = _httpClientFactory.CreateClient("OutletApi");

            var dto = new
            {
                Nombre = model.Nombre,
                Precio = model.Precio,
                PrecioOriginal = model.PrecioOriginal,
                Talla = model.Talla,
                Stock = model.Stock,
                Tipo = model.Tipo,
                Material = model.Material,
                TemporadaId = model.TemporadaId,
                DescuentoPorcentaje = model.DescuentoPorcentaje,
                ImagenBase64 = model.ImagenBase64,
                FileName = model.ImagenFileName
            };

            var resp = await client.PostAsJsonAsync("api/Inventario", dto);
            if (!resp.IsSuccessStatusCode)
            {
                TempData["ApiError"] = await resp.Content.ReadAsStringAsync();
                return View(model);
            }

            TempData["MensajeExito"] = "Producto creado.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            if (!EsEmpleado()) return RedirectToAction("Index", "Home");

            var client = _httpClientFactory.CreateClient("OutletApi");
            var resp = await client.GetAsync($"api/Inventario/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                TempData["ApiError"] = await resp.Content.ReadAsStringAsync();
                return RedirectToAction("Index");
            }

            var json = await resp.Content.ReadAsStringAsync();
            var opciones = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var prod = JsonSerializer.Deserialize<ProductViewModel>(json, opciones);
            if (prod == null) return RedirectToAction("Index");

            var model = new InventarioProductoFormModel
            {
                Id = prod.Id,
                Nombre = prod.Nombre,
                Precio = prod.Precio,
                PrecioOriginal = prod.PrecioOriginal,
                Talla = prod.Talla,
                Stock = prod.Stock,
                Tipo = prod.Tipo,
                Material = prod.Tipo,
                ImagenUrl = prod.ImagenUrl,
                TemporadaId = prod.TemporadaId,
                DescuentoPorcentaje = prod.DescuentoPorcentaje
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, InventarioProductoFormModel model)
        {
            if (!EsEmpleado()) return RedirectToAction("Index", "Home");

            if (!ModelState.IsValid) return View(model);

            var client = _httpClientFactory.CreateClient("OutletApi");

            var dto = new
            {
                Id = id,
                Nombre = model.Nombre,
                Precio = model.Precio,
                PrecioOriginal = model.PrecioOriginal,
                Talla = model.Talla,
                Stock = model.Stock,
                Tipo = model.Tipo,
                Material = model.Material,
                TemporadaId = model.TemporadaId,
                DescuentoPorcentaje = model.DescuentoPorcentaje,
                ImagenBase64 = model.ImagenBase64,
                FileName = model.ImagenFileName
            };

            var resp = await client.PutAsJsonAsync($"api/Inventario/{id}", dto);
            if (!resp.IsSuccessStatusCode)
            {
                TempData["ApiError"] = await resp.Content.ReadAsStringAsync();
                return View(model);
            }

            TempData["MensajeExito"] = "Producto actualizado.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!EsEmpleado()) return RedirectToAction("Index", "Home");

            var client = _httpClientFactory.CreateClient("OutletApi");
            var resp = await client.DeleteAsync($"api/Inventario/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                TempData["ApiError"] = await resp.Content.ReadAsStringAsync();
            }
            else
            {
                TempData["MensajeExito"] = "Producto eliminado.";
            }
            return RedirectToAction("Index");
        }
    }
}