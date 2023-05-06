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
                sw.WriteLine($"{servicio.Cliente.Documento},{servicio.Cliente.Nombre},{servicio.Cliente.Telefono},{servicio.Vehiculo.Placa},{servicio.Vehiculo.Marca},{servicio.Vehiculo.Modelo},{servicio.TipoVehiculo},{servicio.TipoServicio}");
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
    }
}

