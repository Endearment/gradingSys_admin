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
        private static string server = "database-sia-cis.c7gskq208sgz.ap-southeast-2.rds.amazonaws.com";
        private static string uid = " admin";
        private static string password = "05152025CIASIA-admin";
        private static string port = " 3306";

        private static string cisConnectionString = $"Server={server};Database=cis_db;Uid={uid};Pwd={password};port={port}";
     

        public static MySqlConnection GetConnection(string database)
        {
            string connectionString = database switch
            {
                "cis_db" => cisConnectionString,
                _ => throw new ArgumentException("Invalid database name")
            };
            return new MySqlConnection(connectionString);
        }
    }
}
