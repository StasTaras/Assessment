using System;
using System.IO;
using System.Text;

namespace Assessment.Services
{
    public class FileService : IDisposable
    {
        private bool _disposed;
        private readonly FileStream _fileStream;

        public FileService(string filePath)
        {
            _fileStream = File.Open(filePath, FileMode.Open);
            Console.WriteLine("File was opened");
        }

        public void ReadFile()
        {
            var bytes = new byte[1024];
            var utf8Encoding = new UTF8Encoding(true);

            while (_fileStream.Read(bytes, 0, bytes.Length) > 0)
            {
                Console.WriteLine(utf8Encoding.GetString(bytes));
            }
        }

        private void ReleaseUnmanagedResources(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _fileStream.Dispose();
                Console.WriteLine("File was closed");
            }

            _disposed = true;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources(true);
            GC.SuppressFinalize(this);
        }

        ~FileService() => ReleaseUnmanagedResources(false);
    }
}
