using Hrm.Application.Abstractions.Services;
using Hrm.Application.Settings;
using Microsoft.Extensions.Options;

namespace Hrm.Application.Services
{
    internal class LocaleStorageService : IStorageService
    {
        private readonly FileSettings _fileSettings;

        public LocaleStorageService(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;
        }
        public async Task<string> AddFileAsync(Stream stream, string fileName)
        {
            CreateDirectory(_fileSettings.WebRootPath!, _fileSettings.FolderName!);

            string uploadsFolder = Path.Combine(_fileSettings.WebRootPath!, _fileSettings.FolderName!);

            string uniqueFileName = CreateName(fileName, uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            var dbPath = Path.Combine(_fileSettings.FolderName!, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }

            return "/"+dbPath;
        }

        public async Task DeleteFileAsync(string dbPath)
        {
            string deletePath = Path.Combine(_fileSettings.WebRootPath!, dbPath);
            var file = new FileInfo(deletePath);

            if (file.Exists)
            {
                await Task.Factory.StartNew(() => file.Delete());
            }
        }

        private string CreateName(string fileName, string folderPath)
        {
            var path = Path.Combine(folderPath, fileName);

            bool isFileExsist = File.Exists(path);

            if (isFileExsist)
            {
                return $"{Path.GetFileNameWithoutExtension(path)}_" +
                    $"{DateTime.Now.ToString("yyyyMMddTHHmmssfff")}" +
                    $"{Path.GetExtension(path)}";
            }

            return fileName;
        }

        private void CreateDirectory(string webRootPath, string folderPath)
        {
            string path = Path.Combine(webRootPath, folderPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
