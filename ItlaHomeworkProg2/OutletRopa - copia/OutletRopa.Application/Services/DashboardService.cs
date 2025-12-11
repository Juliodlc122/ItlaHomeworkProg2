using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using OutletRopa.Domain.Interfaces;
using OutletRopa.Domain.Entities;
using OutletRopa.Application.Interfaces;
using OutletRopa.Application.DTOs;

namespace OutletRopa.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardDto> ObtenerMetricas()
        {
            var ventas = await _unitOfWork.Repository<Venta>().GetAllAsync();

           
            var productos = await _unitOfWork.Repository<Producto>().GetAllAsync();

            var clientes = await _unitOfWork.Repository<Cliente>().GetAllAsync();

            var dto = new DashboardDto();

            if (ventas != null)
            {
                dto.TotalIngresos = ventas.Sum(v => v.Total);
                dto.CantidadVentas = ventas.Count();

                var fechaInicio = DateTime.Now.AddDays(-7);

                dto.VentasPorFecha = ventas
                    .Where(v => v.Fecha >= fechaInicio)
                    .GroupBy(v => v.Fecha.Date)
                    .Select(g => new VentaDiariaDto
                    {
                        Fecha = g.Key.ToString("dd/MM/yyyy"),
                        TotalVenta = g.Sum(v => v.Total)
                    })
                    .OrderBy(x => x.Fecha)
                    .ToList();
            }

            if (productos != null)
            {
                dto.CantidadProductos = productos.Count();
            }

            if (clientes != null)
            {
                dto.CantidadClientes = clientes.Count();
            }

            return dto;
        }
    }
}