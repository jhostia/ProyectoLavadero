using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        MySqlConnection mySqlConnection;
        public Conexion()
        {
            string server = "localhost";
            string database = "lavadero";
            string user = "root";
            string pwd = "steven123";

            string cadenaConexion = "server=" + server + ";database=" + database + ";Uid=" + user + ";pwd=" + pwd;
            mySqlConnection = new MySqlConnection(cadenaConexion);
        }

        public MySqlConnection AbrirConexion()
        {
            try
            {
                mySqlConnection.Open();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return mySqlConnection;
        }

        public void CerrarConexion()
        {
            mySqlConnection.Close();
        }
    }
}
