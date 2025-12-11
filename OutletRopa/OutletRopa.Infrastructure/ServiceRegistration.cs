using Microsoft.Extensions.DependencyInjection;
using OutletRopa.Application.Interfaces;
using OutletRopa.Infrastructure.Services;

namespace OutletRopa.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<IFileStorageService, FileStorageService>();
        }
    }
}