using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace gradingSys_admin
{
    public class Dbconnection
    {
        private static string server = "localhost";
        private static string database = "cis_db";
        private static string uid = "root";
        private static string password = "";

        private static string connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Database connection error: " + ex.Message);
            }
        }
    }
}
