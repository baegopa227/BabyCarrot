using System.Text;
using System.Data;
using System.Data.SqlClient;
using System;

namespace ClassLibrary1.Tools
{
    public class DBManager
    {
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public static string sql;

        public static Boolean Init()
        {
            try
            {
                Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");

                // Build connection string
                builder.DataSource = "localhost";   // update me
                builder.UserID = "test";              // update me
                builder.Password = "test";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    // Create a sample database
                    Console.Write("Dropping and creating database 'SampleDB' ... ");
                    sql = "DROP DATABASE IF EXISTS [SampleDB]; CREATE DATABASE [SampleDB]";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        
                    }

                    // Create a Table and insert some sample data
                    Console.Write("Creating sample table with data, press any key to continue...");
                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("USE SampleDB; ");
                    sb.Append("CREATE TABLE Members ( ");
                    sb.Append(" ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" name NVARCHAR(50), ");
                    sb.Append(" nickname NVARCHAR(50), ");
                    sb.Append(" password NVARCHAR(50) ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Members (name, nickname, password) VALUES ");
                    sb.Append("(N'test1', N'test1', N'test1'), ");
                    sb.Append("(N'test2', N'test2', N'test2'), ");
                    sb.Append("(N'test3', N'test3', N'test3') ");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        return (command.ExecuteNonQuery()>0)?true:false;
                        Console.WriteLine("Done.");
                    }
                }
            }
            catch (SqlException e)
            {
                return false;
            }

            Console.WriteLine("All done. Press any key to finish...");
            
        }

        public static void Insert()

        {
            // Connect to SQL
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                // INSERT demo
                Console.Write("Inserting a new row into table, press any key to continue...");
                

                StringBuilder sb = new StringBuilder();

                sb.Clear();
                sb.Append("INSERT INTO Members (name, ID, password) ");
                sb.Append("VALUES (@name, @ID, @password);");
                sql = sb.ToString();
                String test_input = "Jake";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", test_input);
                    command.Parameters.AddWithValue("@ID", test_input);
                    command.Parameters.AddWithValue("@password", test_input);
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " row(s) inserted");
                }


            }

        }

        public static void Update()
        {
            // Connect to SQL
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                StringBuilder sb = new StringBuilder();

                // UPDATE demo
                String userToUpdate = "Jake";
                Console.Write("Updating 'password' for user '" + userToUpdate + "', press any key to continue...");
                Console.ReadKey(true);
                sb.Clear();
                sb.Append("UPDATE Mebmers SET password = N'changed' WHERE Name = @name");
                sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", userToUpdate);
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " row(s) updated");
                }

            }
        }

        public static void Delete()
        {
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                StringBuilder sb = new StringBuilder();

                // DELETE demo
                String userToDelete = "Jake";
                Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                Console.ReadKey(true);
                sb.Clear();
                sb.Append("DELETE FROM Members WHERE Name = @name;");
                sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", userToDelete);
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected + " row(s) deleted");
                }

            }

    }

        public static void Select()

        {
            // Connect to SQL
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                // READ demo
                Console.WriteLine("Reading data from table, press any key to continue...");
                Console.ReadKey(true);
                sql = "USE SIMPLEDB; SELECT name, ID, password FROM Members;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }
                    }
                }

            }


        }

        public static string Select(string name)

        {
            // Connect to SQL
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");

                // READ demo
                Console.WriteLine("Reading data from table, press any key to continue...");
               
                sql = "USE SAMPLEDB; SELECT name, nickname, password FROM Members Where name=@name;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("name", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
                return "";
            }


        }

    }
}