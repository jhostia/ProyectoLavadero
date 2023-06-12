using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosEmpleados
    {
        private Conexion conexion;

        public DatosEmpleados()
        {
            conexion = new Conexion();
        }

        public void GuardarEmpleado(Empleado empleado)
        {
            empleado.Disponible = true; // Establecer la disponibilidad como true

            MySqlCommand comando = new MySqlCommand($"INSERT INTO empleados VALUES ({empleado.Id}, '{empleado.Nombre}', " +
                $"'{empleado.Apellido}', '{empleado.Telefono}', {empleado.Disponible})", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }



        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            MySqlCommand comando = new MySqlCommand("SELECT * FROM empleados", conexion.AbrirConexion());
            MySqlDataReader consulta = comando.ExecuteReader();

            while (consulta.Read())
            {

                // Convertir el primer elemento a int
                int id = consulta.GetInt32(0);
                Empleado empleado = new Empleado(id, consulta.GetString(1), consulta.GetString(2), consulta.GetString(3));

                empleados.Add(empleado);
            }
            conexion.CerrarConexion();

            return empleados;
        }

        public void EliminarEmpleado(int id)
        {
            MySqlCommand comando = new MySqlCommand($"DELETE FROM empleados WHERE id = {id}", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
        public void ActualizarEmpleado(Empleado empleado)
        {
            MySqlCommand comando = new MySqlCommand($"UPDATE empleados SET nombre = '{empleado.Nombre}', " +
                $"apellido = '{empleado.Apellido}', telefono = '{empleado.Telefono}', disponible = {empleado.Disponible} " +
                $"WHERE id = {empleado.Id}", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public void EliminarEmpleado(Empleado empleado)
        {
            MySqlCommand comando = new MySqlCommand($"DELETE FROM empleados WHERE id = {empleado.Id}", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }
    }
}
