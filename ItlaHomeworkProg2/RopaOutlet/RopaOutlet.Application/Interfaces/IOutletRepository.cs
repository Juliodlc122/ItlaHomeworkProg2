using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RopaOutlet.Domain;

namespace RopaOutlet.Application.Interfaces
{
    public interface IOutletRepository
    {
        Task<List<Prenda>> GetPrendasAsync();
        Task<Prenda?> GetPrendaByIdAsync(int id);
        Task<bool> ComprarAsync(int prendaId, int cantidad);
    }
}