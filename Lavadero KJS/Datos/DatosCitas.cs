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
            try
            {
                cita.Id = Guid.NewGuid().ToString(); // Generar un nuevo ID único para la cita
                List<Cita> citas = LeerCitas();
                citas.Add(cita);
                GuardarCitas(citas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar la cita: " + ex.Message);
                throw; // Relanzar la excepción para que se propague hacia arriba
            }
        }

        public void ModificarCita(Cita citaModificada)
        {
            List<Cita> citas = LeerCitas();

            for (int i = 0; i < citas.Count; i++)
            {
                if (citas[i].Id == citaModificada.Id)
                {
                    citas[i] = citaModificada;
                    break;
                }
            }

            GuardarCitas(citas);
        }

        public void EliminarCita(string idCita)
        {
            List<Entidades.Cita> citas = LeerCitas();
            Entidades.Cita cita = citas.Find(c => c.Id == idCita);

            if (cita != null)
            {
                citas.Remove(cita);
                GuardarCitas(citas);
            }
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
                string telefonoCliente = Convert.ToString(consulta.GetInt32(3));
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

        private void GuardarCitas(List<Cita> citas)
        {
            foreach (Cita cita in citas)
            {
                MySqlCommand comando = new MySqlCommand($"INSERT INTO citas VALUES ('{cita.Id}', " +
                    $"'{cita.Cliente.Nombre}', {cita.Cliente.Documento}, {cita.Cliente.Telefono}, " +
                    $"'{cita.Vehiculo.Marca}', '{cita.Vehiculo.Modelo}', '{cita.Vehiculo.Placa}', " +
                    $"'{cita.Vehiculo.Tipo}' ,'{cita.Vehiculo.Servicio}', @fecha_cita)", 
                    conexion.AbrirConexion());

                comando.Parameters.AddWithValue("@fecha_cita", cita.FechaHora);
                comando.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }
    }
}
