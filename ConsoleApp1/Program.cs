using System;
using System.Data.SqlClient;
using System.IO;

namespace VarbinaryToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connection string to your SQL Server
            string connectionString = "Data Source={yourDataSource};Initial Catalog={yourDBName};Persist Security Info=False;User ID={user};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // SQL query to retrieve the varbinary data
            string query = "SELECT Bytes FROM YourTable WHERE Id = 12345";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                byte[] fileData = command.ExecuteScalar() as byte[];

                if (fileData != null)
                {
                    string filePath = "D:\\file.pdf"; // e.g., "C:\\Temp\\outputfile.jpg"

                    File.WriteAllBytes(filePath, fileData);

                    Console.WriteLine($"File saved to {filePath}");
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }
    }
}