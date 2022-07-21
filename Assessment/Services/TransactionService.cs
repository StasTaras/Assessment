using System;
using System.Data.SqlClient;

namespace Assessment.Services
{
    public class TransactionService
    {
        private readonly string _connectionString;

        public TransactionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void TransactionWorkExample()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Start a local transaction.
                var sqlTransaction = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                var command = connection.CreateCommand();
                command.Transaction = sqlTransaction;

                try
                {
                    // Execute two separate commands.
                    command.CommandText =
                        "INSERT INTO [asmt].[dbo].[Person](Id, FirstName, LastName, MiddleName, PhoneNumber, Age)" +
                        "VALUES(1, 'Test', 'Test_2', 'Test_3', '+380789878654', 18)";
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO [asmt].[dbo].[Person](Id, FirstName, LastName, MiddleName, PhoneNumber, Age)" +
                        "VALUES(2, 'Test_4', 'Test_5', 'Test_6', '+380789876654', 19)";
                    command.ExecuteNonQuery();

                    // Commit the transaction.
                    sqlTransaction.Commit();
                    Console.WriteLine("Both records were written to database.");
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);

                    try
                    {
                        // Attempt to roll back the transaction.
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection
                        // is closed or the transaction has already been rolled
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }
    }
}
