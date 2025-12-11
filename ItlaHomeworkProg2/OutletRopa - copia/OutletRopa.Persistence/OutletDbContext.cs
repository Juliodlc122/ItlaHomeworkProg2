using Microsoft.EntityFrameworkCore;
using OutletRopa.Domain.Entities;
using System;

namespace OutletRopa.Persistence
{
    public class OutletDbContext : DbContext
    {
        public OutletDbContext(DbContextOptions<OutletDbContext> options) : base(options)
        {
        }

        public DbSet<Camisa> Camisas { get; set; }
        public DbSet<Pantalon> Pantalones { get; set; }
        public DbSet<Zapato> Zapatos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        
        public DbSet<Temporada> Temporadas { get; set; }
        public DbSet<Promocion> Promociones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camisa>().ToTable("Camisas");
            modelBuilder.Entity<Camisa>().Property(c => c.PrecioOriginal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Camisa>().Property(c => c.Precio).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Venta>().Property(v => v.Total).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Pantalon>().ToTable("Pantalones");
            modelBuilder.Entity<Pantalon>().Property(p => p.PrecioOriginal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Pantalon>().Property(p => p.Precio).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Zapato>(b =>
            {
                b.ToTable("Zapatos");
                b.Property(z => z.Precio).HasColumnType("decimal(18,2)");
                b.Property(z => z.PrecioOriginal).HasColumnType("decimal(18,2)");
                
            });

            
            if (modelBuilder.Model.FindEntityType(typeof(Temporada)) != null)
                modelBuilder.Entity<Temporada>().Property(t => t.DescuentoPorcentaje).HasColumnType("decimal(6,2)");
            if (modelBuilder.Model.FindEntityType(typeof(Promocion)) != null)
                modelBuilder.Entity<Promocion>().Property(p => p.DescuentoPorcentaje).HasColumnType("decimal(6,2)");

            
            modelBuilder.Entity<Venta>().HasData(
                new Venta(1, 1, 1500m, "Tarjeta", "PEDIDO-SEED-001")
                {
                    Id = 1,
                    Fecha = DateTime.Now
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}