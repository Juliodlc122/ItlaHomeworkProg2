using System.Threading.Tasks;
using OutletRopa.Application.DTOs;

namespace OutletRopa.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> ObtenerMetricas();
    }
}