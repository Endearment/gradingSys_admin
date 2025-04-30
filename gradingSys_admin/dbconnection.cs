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
        private static string uid = "root";
        private static string password = "";

        private static string cisConnectionString = $"Server={server};Database=cis_db;Uid={uid};Pwd={password};";
        private static string gradingConnectionString = $"Server={server};Database=grading_db;Uid={uid};Pwd={password};";

        public static MySqlConnection GetConnection(string database)
        {
            string connectionString = database switch
            {
                "cis_db" => cisConnectionString,
                "grading_db" => gradingConnectionString,
                _ => throw new ArgumentException("Invalid database name")
            };
            return new MySqlConnection(connectionString);
        }
    }
}
