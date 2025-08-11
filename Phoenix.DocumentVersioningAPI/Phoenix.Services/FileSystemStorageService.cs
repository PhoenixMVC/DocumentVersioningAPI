using System;
using System.IO;
using System.Threading.Tasks;
using Phoenix.Data.Contracts;

namespace Phoenix.Services
{
    public class FileSystemStorageService : IStorageService
    {
        private readonly string _basePath;

        public FileSystemStorageService(string basePath)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            if (fileStream == null || !fileStream.CanRead)
                throw new ArgumentException("Invalid file stream.", nameof(fileStream));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            var filePath = Path.Combine(_basePath, fileName);

            using (var output = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, useAsync: true))
            {
                await fileStream.CopyToAsync(output);
            }

            return filePath; // مسیر کامل فایل ذخیره‌شده
        }
    }
}
