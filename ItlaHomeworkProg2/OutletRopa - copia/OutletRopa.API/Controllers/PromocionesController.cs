using Microsoft.AspNetCore.Mvc;
using OutletRopa.Domain.Entities;
using OutletRopa.Domain.Interfaces;
using System.Threading.Tasks;

namespace OutletRopa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromocionesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}