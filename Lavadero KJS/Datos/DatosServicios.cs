using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosServicios
    {
        private readonly string _rutaArchivo = "servicios.txt";

        public void AgregarServicio(Servicio servicio)
        {
            using (StreamWriter sw = File.AppendText(_rutaArchivo))
            {
                // Escribir los datos del servicio en el archivo de texto
                sw.WriteLine($"{servicio.Cliente.Documento},{servicio.Cliente.Nombre},{servicio.Cliente.Telefono},{servicio.Vehiculo.Placa},{servicio.Vehiculo.Marca},{servicio.Vehiculo.Modelo},{servicio.Vehiculo.Tipo},{servicio.TipoServicio}");
            }
        }

        public List<Servicio> ObtenerServicios()
        {
            List<Servicio> servicios = new List<Servicio>();

            using (StreamReader sr = new StreamReader(_rutaArchivo))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datosServicio = linea.Split(',');
                    Cliente cliente = new Cliente(datosServicio[0], datosServicio[1], datosServicio[2]);
                    Vehiculo vehiculo = new Vehiculo(datosServicio[4], datosServicio[5], datosServicio[3], (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), datosServicio[6]));
                    Servicio servicio = new Servicio(cliente, vehiculo, datosServicio[6], datosServicio[7]);
                    servicios.Add(servicio);
                }
            }

            return servicios;
        }

        public Factura GenerarFactura()
        {
            // Obtener el último servicio agregado
            List<Servicio> servicios = ObtenerServicios();
            Servicio ultimoServicio = servicios.LastOrDefault();

            // Verificar si hay al menos un servicio registrado
            if (ultimoServicio != null)
            {
                // Crear una nueva instancia de Factura y devolverla
                return new Factura(
                    "Lavadero KJS", // Lavadero
                    DateTime.Now, // Fecha
                    ultimoServicio, // Servicio
                    ultimoServicio.Cliente.Nombre, // NombreCliente
                    "", // Direccion (en blanco)
                    "", // Telefono (en blanco)
                    "Gracias por preferirnos" // Mensaje
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

            List<Servicio> servicios = ObtenerServicios();

            // Buscar el servicio a actualizar por su placa
            for (int i = 0; i < servicios.Count; i++)
            {
                if (servicios[i].Vehiculo.Placa == servicio.Vehiculo.Placa)
                {
                    // Actualizar el servicio con los nuevos datos
                    servicios[i] = servicio;
                    break;
                }
            }

            // Guardar los servicios actualizados en el archivo
            using (StreamWriter sw = new StreamWriter(_rutaArchivo))
            {
                foreach (Servicio serv in servicios)
                {
                    sw.WriteLine($"{serv.Cliente.Documento},{serv.Cliente.Nombre},{serv.Cliente.Telefono},{serv.Vehiculo.Placa},{serv.Vehiculo.Marca},{serv.Vehiculo.Modelo},{serv.Vehiculo.Tipo},{serv.TipoServicio}");
                }
            }
        }

        public void EliminarServicio(Servicio servicio)
        {
            // Obtener todos los servicios del archivo
            List<Servicio> servicios = ObtenerServicios();

            // Buscar el servicio a eliminar
            Servicio servicioAEliminar = servicios.FirstOrDefault(s => s.Vehiculo.Placa == servicio.Vehiculo.Placa);

            // Verificar si se encontró el servicio a eliminar
            if (servicioAEliminar != null)
            {
                // Remover el servicio de la lista
                servicios.Remove(servicioAEliminar);

                // Volver a escribir todos los servicios en el archivo
                using (StreamWriter sw = new StreamWriter(_rutaArchivo))
                {
                    foreach (Servicio servicioActualizado in servicios)
                    {
                        sw.WriteLine($"{servicioActualizado.Cliente.Documento},{servicioActualizado.Cliente.Nombre},{servicioActualizado.Cliente.Telefono},{servicioActualizado.Vehiculo.Placa},{servicioActualizado.Vehiculo.Marca},{servicioActualizado.Vehiculo.Modelo},{servicioActualizado.Vehiculo.Tipo},{servicioActualizado.TipoServicio}");
                    }
                }
            }
        }
    }
}


