using Microsoft.EntityFrameworkCore;
using RopaOutlet.Application.Interfaces;
using RopaOutlet.Domain;

namespace RopaOutlet.Persistence.Repositories
{
    public class OutletRepository : IOutletRepository
    {
        private readonly OutletDbContext _context;

        public OutletRepository(OutletDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prenda>> GetPrendasAsync()
        {
            return await _context.Prendas.ToListAsync();
        }

        public async Task<Prenda?> GetPrendaByIdAsync(int id)
        {
            return await _context.Prendas.FindAsync(id);
        }

        public async Task<bool> ComprarAsync(int prendaId, int cantidad)
        {
            var prenda = await _context.Prendas.FindAsync(prendaId);
            if (prenda == null || prenda.Stock < cantidad) return false;

            
            prenda.Stock -= cantidad;

            var venta = new Venta
            {
                PrendaId = prendaId,
                Cantidad = cantidad,
                TotalPagado = prenda.PrecioFinal * cantidad,
                Fecha = DateTime.Now
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}