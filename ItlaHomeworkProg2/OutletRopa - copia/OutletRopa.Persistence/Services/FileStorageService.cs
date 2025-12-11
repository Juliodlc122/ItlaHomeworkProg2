using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OutletRopa.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OutletRopa.Persistence.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string containerName)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            string folder = Path.Combine(_env.WebRootPath ?? string.Empty, "imagenes", containerName);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(folder, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                await File.WriteAllBytesAsync(filePath, memoryStream.ToArray());
            }

            var request = _httpContextAccessor.HttpContext?.Request;
            var scheme = request?.Scheme ?? "https";
            var host = request?.Host.Value ?? "localhost";
            return $"{scheme}://{host}/imagenes/{containerName}/{fileName}";
        }

        public Task DeleteFileAsync(string route, string containerName)
        {
            if (string.IsNullOrEmpty(route)) return Task.CompletedTask;
            var fileName = Path.GetFileName(route);
            var filePath = Path.Combine(_env.WebRootPath ?? string.Empty, "imagenes", containerName, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            return Task.CompletedTask;
        }

        public async Task<string> EditFileAsync(IFormFile file, string route, string containerName)
        {
            await DeleteFileAsync(route, containerName);
            return await SaveFileAsync(file, containerName);
        }
    }
}