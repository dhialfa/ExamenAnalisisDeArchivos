using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace Examen.Models
{
    public static class SecurityConfig
    {
        private static string connectionString = "server=saacapps.com;UserID=saacapps_ucatolica;Database=saacapps_training;Password=Ucat0lica";

        /// <summary>
        /// Manage the DB connection
        /// </summary>
        /// <returns>A DB connection object like MySqlConnection</returns>

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}