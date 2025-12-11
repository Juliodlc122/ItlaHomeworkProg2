using Microsoft.EntityFrameworkCore;
using OutletRopa.Domain.Entities;

namespace OutletRopa.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

       
        public DbSet<Producto> Productos { get; set; }

        
        public DbSet<Camisa> Camisas { get; set; }
        public DbSet<Pantalon> Pantalones { get; set; }
        public DbSet<Zapato> Zapatos { get; set; }

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Producto>().Property(p => p.PrecioOriginal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Producto>().Property(p => p.DescuentoPorcentaje).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Venta>().Property(v => v.Total).HasColumnType("decimal(18,2)");
        }
    }
}