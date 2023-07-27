namespace Hrm.Application.Abstractions.Services
{
    internal interface IStorageService
    {
        Task<string> AddFileAsync(Stream stream, string fileName);
        Task DeleteFileAsync(string dbPath);
    }
}