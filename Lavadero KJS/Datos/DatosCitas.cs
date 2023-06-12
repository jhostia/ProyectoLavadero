using System;
using System.Collections.Generic;
using Entidades;
using MySql.Data.MySqlClient;

namespace Datos
{
    public class DatosCitas
    {
        private Conexion conexion;


        public DatosCitas()
        {
            this.conexion = new Conexion();
        }
    
        public List<Cita> ObtenerCitas()
        {
            return LeerCitas();
        }

        public void AgregarCita(Cita cita)
        {
            MySqlCommand comando = new MySqlCommand($"INSERT INTO citas VALUES ('{cita.Id}', " +
                    $"'{cita.Cliente.Nombre}', {cita.Cliente.Documento}, '{cita.Cliente.Telefono}', " +
                    $"'{cita.Vehiculo.Marca}', '{cita.Vehiculo.Modelo}', '{cita.Vehiculo.Placa}', " +
                    $"'{cita.Vehiculo.Tipo}' ,'{cita.Vehiculo.Servicio}', @fecha_cita)",
                    conexion.AbrirConexion());

            comando.Parameters.AddWithValue("@fecha_cita", cita.FechaHora);
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void ModificarCita(Cita cita)
        {
            MySqlCommand comando = new MySqlCommand($"UPDATE citas SET id = '{cita.Id}', " +
                   $"nombre_cliente = '{cita.Cliente.Nombre}', documento_cliente = {cita.Cliente.Documento}, " +
                   $"telefono_cliente = '{cita.Cliente.Telefono}', " +
                   $"marca_vehiculo = '{cita.Vehiculo.Marca}', modelo_vehiculo = '{cita.Vehiculo.Modelo}', " +
                   $"matricula_vehiculo = '{cita.Vehiculo.Placa}', " +
                   $"tipo_vehiculo = '{cita.Vehiculo.Tipo}' ,tipo_servicio = '{cita.Vehiculo.Servicio}', fecha_cita = @fecha_cita " +
                   $"WHERE id = {cita.Id}",
                   conexion.AbrirConexion());

            comando.Parameters.AddWithValue("@fecha_cita", cita.FechaHora);
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void EliminarCita(string idCita)
        {
            MySqlCommand comando = new MySqlCommand($"DELETE FROM citas WHERE id = '{idCita}'", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        private List<Cita> LeerCitas()
        {
            List<Cita> citas = new List<Cita>();

            MySqlCommand comando = new MySqlCommand("SELECT * FROM citas", conexion.AbrirConexion());
            MySqlDataReader consulta = comando.ExecuteReader();

            while (consulta.Read())
            {

                string id = consulta.GetString(0);
                string nombreCliente = consulta.GetString(1);
                string documentoCliente = Convert.ToString(consulta.GetInt32(2));
                string telefonoCliente = consulta.GetString(3);
                string marcaVehiculo = consulta.GetString(4);
                string modeloVehiculo = consulta.GetString(5);
                string placaVehiculo = consulta.GetString(6);
                string tipoVehiculo = consulta.GetString(7);
                string tipoServicio = consulta.GetString(8);
                DateTime fechaHora = consulta.GetDateTime(9);

                Cliente cliente = new Cliente(nombreCliente, documentoCliente, telefonoCliente);
                Vehiculo vehiculo = new Vehiculo(marcaVehiculo, modeloVehiculo, placaVehiculo, 
                    (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), tipoVehiculo));
                Cita cita = new Cita(cliente, vehiculo, tipoServicio, fechaHora, id);

                citas.Add(cita);
            }
            conexion.CerrarConexion();

            return citas;
        }
    }
}
