using Microsoft.AspNetCore.Mvc;
using OutletRopa.Domain.Interfaces;
using OutletRopa.Domain.Entities; 
using System.Threading.Tasks;

namespace OutletRopa.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TemporadasController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public TemporadasController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		
	}
}