using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaEmpleados
    {
        private readonly DatosEmpleados _datosEmpleados;
        public LogicaEmpleados()
        {
            _datosEmpleados = new DatosEmpleados();
        }

        public void GuardarEmpleado(Empleado empleado)
        {
            if (empleado.Id == 0 || string.IsNullOrEmpty(empleado.Nombre) || string.IsNullOrEmpty(empleado.Apellido) || string.IsNullOrEmpty(empleado.Telefono))
            {
                throw new ArgumentException("Por favor complete todos los campos.");
            }

            if (EmpleadoExiste(empleado.Id))
            {
                throw new ArgumentException("El empleado ya existe.");
            }

            _datosEmpleados.GuardarEmpleado(empleado);
        }

        public bool EmpleadoExiste(int idEmpleado)
        {
            List<Empleado> empleados = _datosEmpleados.ObtenerEmpleados();
            return empleados.Any(u => u.Id == idEmpleado);
        }

        public List<Empleado> ObtenerEmpleados()
        {
            return _datosEmpleados.ObtenerEmpleados();
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            // Iterar sobre la lista de servicios y buscar el servicio con la placa especificada
            foreach (Empleado empleado in _datosEmpleados.ObtenerEmpleados())
            {
                if (empleado.Id == id)
                {
                    return empleado;
                }
            }
            // Si no se encuentra ningún servicio con la placa especificada, retornar null o lanzar una excepción
            return null;
        }
        public void ActualizarEmpleado(Empleado empleado)
        {
            if (empleado.Id == 0 || string.IsNullOrEmpty(empleado.Nombre) || string.IsNullOrEmpty(empleado.Apellido) || string.IsNullOrEmpty(empleado.Telefono))
            {
                throw new ArgumentException("Por favor complete todos los campos.");
            }

            _datosEmpleados.ActualizarEmpleado(empleado);
        }

        public void EliminarEmpleado(Empleado empleado)
        {
            _datosEmpleados.EliminarEmpleado(empleado);
        }
    }
}
