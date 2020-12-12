using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace Administracion_Alumnos
{
    public static class ConnectionDB
    {
        private static string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=alumnos_admin;";

        public static DataTable ExecuteQuery(string query)
        {
            // Prepara la conexión
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            // Abre la base de datos
            databaseConnection.Open();
            MySqlDataAdapter adp = new MySqlDataAdapter(query, databaseConnection);
            DataTable consulta = new DataTable();
            adp.Fill(consulta);
            // Cerrar la conexión
            databaseConnection.Close();
            return consulta;
        }

        public static void ExecuteNonQuery(string act)
        {
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(act, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            databaseConnection.Open();
            MySqlDataReader myReader = commandDatabase.ExecuteReader();
            databaseConnection.Close();
        }
    }
}
