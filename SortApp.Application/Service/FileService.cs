using SortApp.Application.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortApp.Application.Service
{
    public class FileService : IFileService
    {
        private const string SEPARATOR = " ";
        private readonly FileServiceConfig _config;
        private readonly object _fileLock = new();

        public FileService(FileServiceConfig config)
        {
            _config = config;
        }

        public void SaveNumberArrayToFile(IEnumerable<long> numbers)
        {
            lock (_fileLock)
            {
                EnsureIsDirectoryExists();
                File.WriteAllText(GetFilePath(), string.Join(SEPARATOR, numbers));
            }
        }

        private void EnsureIsDirectoryExists()
        {
            if (!string.IsNullOrEmpty(_config.DirPath) && !Directory.Exists(_config.DirPath))
            {
                Directory.CreateDirectory(_config.DirPath);
            }
        }

        private string GetFilePath() => Path.Combine(_config.DirPath, _config.FileName);

        public IEnumerable<long> GetLastSavedFileContent()
        {
            lock (_fileLock)
            {
                if (!File.Exists(GetFilePath()))
                {
                    return Enumerable.Empty<long>();
                }

                var fileContentStr = File.ReadAllText(GetFilePath());
                return fileContentStr
                    .Split(SEPARATOR)
                    .Select(p => long.TryParse(p, out long longVal) ? longVal : throw new Exception("Saved file contains non convertible item"));
            }
        }

    }
}
