using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutletRopa.Domain.Interfaces;
using OutletRopa.Persistence.Contexts;
using OutletRopa.Persistence.Repositories;

namespace OutletRopa.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            #region Repositories
            
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion
        }
    }
}