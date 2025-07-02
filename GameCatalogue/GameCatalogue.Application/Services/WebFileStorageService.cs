using GameCatalogue.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace GameCatalogue.Application.Services
{
    /// <summary>
    /// Implementation of IFileStorageService to save games files on wwwroot folder.
    /// In a reall application a cloud storage implementation should be used for scalability
    /// </summary>
    public class WebFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        public WebFileStorageService(IWebHostEnvironment env) => _env = env;

        public async Task<string> SaveImageAsync(long gameId, Stream fileStream, string extension)
        {
            var folder = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(folder);
            var fileName = $"{gameId}_{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(folder, fileName);
            using var fs = File.Create(fullPath);
            await fileStream.CopyToAsync(fs);
            return Path.Combine("/images", fileName).Replace("\\", "/");
        }

        public Task DeleteFileAsync(string relativePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath, relativePath);
            if (File.Exists(fullPath)) File.Delete(fullPath);
            return Task.CompletedTask;
        }
    }

}
