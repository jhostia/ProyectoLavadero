using Entidades;
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
        private readonly string _rutaArchivo = "Empleados.txt";

        public void GuardarEmpleado(Empleado empleado)
        {
            using (StreamWriter sw = File.AppendText(_rutaArchivo))
            {
                // Escribir los datos del empleado en el archivo de texto
                sw.WriteLine($"{empleado.Id},{empleado.Nombre},{empleado.Apellido},{empleado.Telefono}");
            }
        }


        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();

            using (StreamReader sr = new StreamReader(_rutaArchivo))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datosEmpleado = linea.Split(',');

                    // Convertir el primer elemento a int
                    int id = int.Parse(datosEmpleado[0]);
                    Empleado empleado = new Empleado(id, datosEmpleado[1], datosEmpleado[2], datosEmpleado[3]);

                    empleados.Add(empleado);
                }
            }

            return empleados;
        }

        public void EliminarEmpleado(int id)
        {
            List<Empleado> empleados = ObtenerEmpleados();
            Empleado empleado = empleados.FirstOrDefault(e => e.Id == id);
            if (empleado != null)
            {
                empleados.Remove(empleado);
                GuardarEmpleado(empleado);
            }
        }
        public void ActualizarEmpleado(Empleado empleado)
        {
            List<Empleado> empleados = ObtenerEmpleados();
            for (int i = 0; i < empleados.Count; i++)
            {
                if (empleados[i].Id == empleado.Id)
                {
                    // Actualizar el servicio con los nuevos datos
                    empleados[i] = empleado;
                    break;
                }
            }

            // Guardar los servicios actualizados en el archivo
            using (StreamWriter sw = new StreamWriter(_rutaArchivo))
            {
                foreach (Empleado serv in empleados)
                {
                    sw.WriteLine($"{serv.Id},{serv.Nombre},{serv.Apellido},{serv.Telefono}");
                }
            }
        }

        public void EliminarEmpleado(Empleado empleado)
        {
            // Obtener todos los servicios del archivo
            List<Empleado> empleados = ObtenerEmpleados();

            // Buscar el servicio a eliminar
            Empleado empleadoAEliminar = empleados.FirstOrDefault(s => s.Id == empleado.Id);

            // Verificar si se encontró el servicio a eliminar
            if (empleadoAEliminar != null)
            {
                // Remover el servicio de la lista
                empleados.Remove(empleadoAEliminar);

                // Volver a escribir todos los servicios en el archivo
                using (StreamWriter sw = new StreamWriter(_rutaArchivo))
                {
                    foreach (Empleado empleadoActualizado in empleados)
                    {
                        sw.WriteLine($"{empleadoActualizado.Id},{empleadoActualizado.Nombre},{empleadoActualizado.Apellido},{empleadoActualizado.Telefono}");
                    }
                }
            }
        }
    }
}
