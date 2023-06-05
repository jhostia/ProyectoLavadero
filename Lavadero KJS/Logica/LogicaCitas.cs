using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaCitas
    {
        private DatosCitas datosCitas; // Instancia de la clase DatosCitas para acceder a los métodos de acceso a datos

        public LogicaCitas()
        {
            datosCitas = new DatosCitas(); // Inicializar la instancia de DatosCitas
        }

        public List<Cita> ObtenerCitas()
        {
            return DatosCitas.ObtenerCitas(); // Llamar al método estático de la capa de datos para obtener las citas
        }


        public void AgregarCita(Cliente cliente, Vehiculo vehiculo, string tipoServicio, DateTime fechaHora)
        {
            string id = GenerarIdCita(); // Generar el ID de la cita

            Cita cita = new Cita(cliente, vehiculo, tipoServicio, fechaHora, id); // Crear una instancia de la clase Cita con los datos proporcionados

            DatosCitas.AgregarCita(cita); // Llamar al método de la capa de datos para agregar la cita
        }

        public void ModificarCita(string id, Cliente cliente, Vehiculo vehiculo, string tipoServicio, DateTime fechaHora)
        {
            Cita cita = new Cita(cliente, vehiculo, tipoServicio, fechaHora, id); // Crear una instancia de la clase Cita con los datos proporcionados

            DatosCitas.ModificarCita(cita); // Llamar al método de la capa de datos para modificar la cita
        }

        public void EliminarCita(string id)
        {
            DatosCitas.EliminarCita(id); // Llamar al método de la capa de datos para eliminar la cita
        }

        private string GenerarIdCita()
        {
            // Generar un ID único para la cita, por ejemplo, utilizando una combinación de la fecha y hora actual
            string id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return id;
        }
    }
}
