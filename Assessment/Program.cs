using System;
using System.IO;
using Assessment.Services;

namespace Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //to use UnmanagedMemoryService
            Console.WriteLine("1. Unmanaged memory allocation and release example:");
            using (var unmanagedMemoryService = new  UnmanagedMemoryService(10 * 1024 * 1024))
            {
                //TODO: some logic to use unmanagedMemoryService
            }

            Console.Read();

            //to use FileService
            Console.WriteLine("2. Open/Close file example:");
            using (var fileService = new FileService(Directory.GetCurrentDirectory() + "\\Files\\AssessmentTempFile.txt"))
            {
                fileService.ReadFile();
            }

            Console.Read();

            //to use FileService
            Console.WriteLine("3. Open/Close/Rollback transactions example:");
            const string dbConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=asmt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var transactionService = new TransactionService(dbConnection);
            transactionService.TransactionWorkExample();

            Console.Read();
        }
    }
}
