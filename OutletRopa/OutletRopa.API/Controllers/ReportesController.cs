using Microsoft.AspNetCore.Mvc;
using OutletRopa.Application.Interfaces;
using System.Threading.Tasks;

namespace OutletRopa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public ReportesController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("resumen")]
        public async Task<IActionResult> ObtenerResumen()
        {
            var resultado = await _dashboardService.ObtenerMetricas();
            return Ok(resultado);
        }
    }
}