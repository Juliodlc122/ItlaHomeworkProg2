using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OutletRopa.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string containerName);
        Task DeleteFileAsync(string route, string containerName);
    }
}