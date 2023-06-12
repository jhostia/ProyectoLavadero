using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datos
{
    public class DatosServicios
    {
        private Conexion conexion;
        public DatosServicios()
        {
            conexion = new Conexion();
        }

        public void AgregarServicio(Servicio servicio)
        {
            if (string.IsNullOrEmpty(servicio.Vehiculo.Placa) || string.IsNullOrEmpty(servicio.Vehiculo.Marca) || string.IsNullOrEmpty(servicio.Vehiculo.Modelo)
                || string.IsNullOrEmpty(servicio.Cliente.Nombre) || string.IsNullOrEmpty(servicio.Cliente.Documento) || string.IsNullOrEmpty(servicio.Cliente.Telefono))
            {
                throw new ArgumentException("Por favor complete todos los campos.");
            }

            // Obtener todos los servicios existentes
            List<Servicio> servicios = ObtenerServicios();

            // Verificar si el servicio ya existe por su placa
            bool servicioExistente = servicios.Any(s => s.Vehiculo.Placa == servicio.Vehiculo.Placa);
            if (servicioExistente)
            {
                throw new ArgumentException("Ya existe un servicio con esa placa.");
            }

            // Guardar todos los servicios, incluyendo el nuevo, en el archivo
            MySqlCommand comando = new MySqlCommand($"INSERT INTO servicios VALUES ('{servicio.Cliente.Nombre}', " +
                $"'{servicio.Cliente.Documento}', '{servicio.Cliente.Telefono}', " +
                $"'{servicio.Vehiculo.Marca}', '{servicio.Vehiculo.Modelo}', " +
                $"'{servicio.Vehiculo.Placa}', '{servicio.Vehiculo.Tipo}', " +
                $"'{servicio.TipoServicio}', {servicio.EsUrgente}, {servicio.Prioridad}, {servicio.ValorAdicional})", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }


        public List<Servicio> ObtenerServicios()
        {
            List<Servicio> servicios = new List<Servicio>();

            MySqlCommand comando = new MySqlCommand("SELECT * FROM servicios", conexion.AbrirConexion());
            MySqlDataReader consulta = comando.ExecuteReader();

            while (consulta.Read())
            {

                Cliente cliente = new Cliente(consulta.GetString(0), consulta.GetString(1), consulta.GetString(2));

                Vehiculo vehiculo = new Vehiculo(consulta.GetString(3), consulta.GetString(4), consulta.GetString(5), 
                    (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), consulta.GetString(6)));

                string tipoServicio = consulta.GetString(7);
                string tipoVehiculo = consulta.GetString(6);
                bool esUrgente = consulta.GetBoolean(8); // Comprobar si el índice existe y convertir a bool
                int prioridad = consulta.GetInt32(9); // Comprobar si el índice existe y convertir a int
                decimal valorAdicional = consulta.GetDecimal(10); // Comprobar si el índice existe y convertir a decimal

                Servicio servicio = new Servicio(cliente, vehiculo, tipoVehiculo, tipoServicio)
                {
                    EsUrgente = esUrgente,
                    Prioridad = prioridad,
                    ValorAdicional = Convert.ToInt32(valorAdicional)

                };
                servicios.Add(servicio);
            }
            conexion.CerrarConexion();

            return servicios;
        }

        public Factura GenerarFactura(Servicio servicio)
        {
          
            // Verificar si hay al menos un servicio registrado
            if (servicio != null)
            {
                // Calcular el costo total sumando el costo del servicio y el valor adicional
                decimal costoTotal = servicio.Costo + servicio.ValorAdicional;

                // Crear una nueva instancia de Factura y devolverla
                return new Factura(
                    "Lavadero KJS", // Lavadero
                    DateTime.Now, // Fecha
                    servicio, // Servicio
                    servicio.Cliente.Nombre, // NombreCliente
                    "", // Direccion (en blanco)
                    "", // Telefono (en blanco)
                    "Gracias por preferirnos", // Mensaje
                    servicio.Costo, // Costo del servicio
                    servicio.ValorAdicional, // Valor adicional
                    costoTotal // Costo total
                );
            }
            else
            {
                // Si no hay servicios registrados, devolver null
                return null;
            }
        }


        public void ActualizarServicio(Servicio servicio)
        {
            if (string.IsNullOrEmpty(servicio.Vehiculo.Placa) || string.IsNullOrEmpty(servicio.Vehiculo.Marca) || string.IsNullOrEmpty(servicio.Vehiculo.Modelo)
                || string.IsNullOrEmpty(servicio.Cliente.Nombre) || string.IsNullOrEmpty(servicio.Cliente.Documento) || string.IsNullOrEmpty(servicio.Cliente.Telefono))
            {
                throw new ArgumentException("Por favor complete todos los campos.");
            }

            MySqlCommand comando = new MySqlCommand($"UPDATE servicios SET nombre_cliente = '{servicio.Cliente.Nombre}', " +
                $"documento_cliente = '{servicio.Cliente.Documento}', telefono_cliente = '{servicio.Cliente.Telefono}', " +
                $"marca_vehiculo = '{servicio.Vehiculo.Marca}', modelo_vehiculo = '{servicio.Vehiculo.Modelo}', " +
                $"matricula_vehiculo = '{servicio.Vehiculo.Placa}', tipo_vehiculo = '{servicio.Vehiculo.Tipo}', " +
                $"tipo_servicio = '{servicio.TipoServicio}', urgente = {servicio.EsUrgente}, " +
                $"prioridad = {servicio.Prioridad}, precio_adicional = {servicio.ValorAdicional} " +
                $"WHERE matricula_vehiculo = '{servicio.Vehiculo.Placa}'", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void EliminarServicio(Servicio servicio)
        {
            // Obtener todos los servicios de la base
            List<Servicio> servicios = ObtenerServicios();

            // Buscar el servicio a eliminar
            Servicio servicioAEliminar = servicios.FirstOrDefault(s => s.Vehiculo.Placa == servicio.Vehiculo.Placa);

            // Verificar si se encontró el servicio a eliminar
            if (servicioAEliminar != null)
            {
                // Remover el servicio de la lista
                servicios.Remove(servicioAEliminar);

                MySqlCommand comando = new MySqlCommand($"DELETE FROM servicios WHERE " +
                    $"matricula_vehiculo = '{servicio.Vehiculo.Placa}'", conexion.AbrirConexion());
                comando.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }

        public List<Empleado> ObtenerEmpleadosDisponibles()
        {
            List<Empleado> empleados = new List<Empleado>();

            // Leer la base de datos y obtener los empleados disponibles
            MySqlCommand comando = new MySqlCommand("SELECT * FROM empleados WHERE disponible = 1", conexion.AbrirConexion());
            MySqlDataReader consulta = comando.ExecuteReader();
            while (consulta.Read())
            {
                int id = consulta.GetInt32(0);
                string nombre = consulta.GetString(1);
                bool disponible = consulta.GetBoolean(2);

                Empleado empleado = new Empleado(id, nombre, disponible);
                empleados.Add(empleado);
            }
            conexion.CerrarConexion();

            return empleados;
        }

        public void ActualizarEmpleado(Empleado empleado)
        {
            MySqlCommand comando = new MySqlCommand($"UPDATE empleados SET id = {empleado.Id}, " +
                $"nombre = '{empleado.Nombre}', apellido = '{empleado.Apellido}', " +
                $"telefono = '{empleado.Telefono}', disponible = {empleado.Disponible}", conexion.AbrirConexion());
            conexion.CerrarConexion();
        }


    }

}


