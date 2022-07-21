using System;
using System.Runtime.InteropServices;

namespace Assessment.Services
{
    /*
     * UnmanagedMemoryService it is the service to show how to allocate/release unmanaged memory of the process
     *
     * Disposed pattern was used here to release of the unmanaged memory and to avoid memory leaks
     *
     * Created by:      Stanislav Tarasenko
     * Creation Date:   07/21/2022
     * Usage:           To use object of the class it is required to use "using" block and creating
     *                  instance of the object it is required to pass size of the required unmanaged memory
     */
    public class UnmanagedMemoryService : IDisposable
    {
        private readonly IntPtr _handle;
        private bool _disposed;

        public UnmanagedMemoryService(int size)
        {
            //Allocates memory from the unmanaged memory of the process by using the specific number of bytes
            _handle = Marshal.AllocHGlobal(size);
            Console.WriteLine("Unmanaged memory was allocated");
        }

        ~UnmanagedMemoryService()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                //Release memory previously allocated from unmanaged memory of the process
                Marshal.FreeHGlobal(_handle);
                _disposed = true;
                Console.WriteLine("Unmanaged memory was released");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
