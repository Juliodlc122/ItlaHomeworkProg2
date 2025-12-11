using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RopaOutlet.Domain;

namespace RopaOutlet.Persistence
{
    public class OutletDbContext : DbContext
    {
        public OutletDbContext(DbContextOptions<OutletDbContext> options) : base(options) { }

        public DbSet<Prenda> Prendas { get; set; }
        public DbSet<Venta> Ventas { get; set; }
    }
}