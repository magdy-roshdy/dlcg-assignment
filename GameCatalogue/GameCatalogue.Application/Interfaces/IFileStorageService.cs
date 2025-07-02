namespace GameCatalogue.Application.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveImageAsync(long gameId, Stream fileStream, string extension);
        Task DeleteFileAsync(string relativePath);
    }
}
