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
            string query = "SELECT Bytes, Filename FROM YourTable WHERE Id BETWEEN 72240 AND 72250";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] fileData = reader["Bytes"] as byte[];
                        string fileName = reader["Filename"] as string;

                        if (fileData != null && !string.IsNullOrEmpty(fileName))
                        {
                            string filePath = Path.Combine("D:\\", fileName); // Save the file to the D: drive

                            File.WriteAllBytes(filePath, fileData);

                            Console.WriteLine($"File saved to {filePath}");
                        }
                        else
                        {
                            Console.WriteLine("No data found for one of the rows.");
                        }
                    }
                }
            }
        }
    }
}