using Entidades;
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
        private readonly string _rutaArchivo = "servicios.txt";
        

        

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

            // Agregar el nuevo servicio a la lista
            servicios.Add(servicio);

            // Guardar todos los servicios, incluyendo el nuevo, en el archivo
            using (StreamWriter sw = new StreamWriter(_rutaArchivo))
            {
                foreach (Servicio serv in servicios)
                {
                    string esUrgenteString = serv.EsUrgente ? "1" : "0";
                    string valorAdicionalString = serv.ValorAdicional.ToString();
                    string costoServicioString = serv.CalcularCosto().ToString();
                    string costoTotalString = serv.CalcularValorTotal().ToString();
                    string fechaServicioString = serv.ObtenerFechaServicio();
                    string horaServicioString = serv.ObtenerHoraServicio();

                    sw.WriteLine($"{serv.Cliente.Documento},{serv.Cliente.Nombre},{serv.Cliente.Telefono},{serv.Vehiculo.Placa},{serv.Vehiculo.Marca},{serv.Vehiculo.Modelo},{serv.Vehiculo.Tipo},{serv.TipoServicio},{esUrgenteString},{serv.Prioridad},{valorAdicionalString},{costoServicioString},{costoTotalString},{fechaServicioString},{horaServicioString}");
                }
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

                    Cliente cliente = new Cliente(datosServicio[1], datosServicio[0], datosServicio[2]);
                    Vehiculo vehiculo = new Vehiculo(datosServicio[3], datosServicio[4], datosServicio[5], (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), datosServicio[6]));
                    string tipoServicio = datosServicio[7];
                    string tipoVehiculo = datosServicio[6];

                    bool esUrgente = datosServicio.Length > 8 && datosServicio[8] == "1"; // Comprobar si el índice existe y convertir a bool
                    int prioridad = datosServicio.Length > 9 ? int.Parse(datosServicio[9]) : 0; // Comprobar si el índice existe y convertir a int
                    decimal valorAdicional = datosServicio.Length > 10 ? decimal.Parse(datosServicio[10]) : 0; // Comprobar si el índice existe y convertir a decimal

                    Servicio servicio = new Servicio(cliente, vehiculo, tipoVehiculo, tipoServicio)
                    {
                        EsUrgente = esUrgente,
                        Prioridad = prioridad,
                        ValorAdicional = Convert.ToInt32(valorAdicional)

                    };
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
                // Calcular el costo total sumando el costo del servicio y el valor adicional
                decimal costoTotal = ultimoServicio.Costo + ultimoServicio.ValorAdicional;

                // Crear una nueva instancia de Factura y devolverla
                return new Factura(
                    "Lavadero KJS", // Lavadero
                    DateTime.Now, // Fecha
                    ultimoServicio, // Servicio
                    ultimoServicio.Cliente.Nombre, // NombreCliente
                    "", // Direccion (en blanco)
                    "", // Telefono (en blanco)
                    "Gracias por preferirnos", // Mensaje
                    ultimoServicio.Costo, // Costo del servicio
                    ultimoServicio.ValorAdicional, // Valor adicional
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

            List<Servicio> servicios = ObtenerServicios();

            // Buscar el servicio a actualizar por su placa
            for (int i = 0; i < servicios.Count; i++)
            {
                if (servicios[i].Vehiculo.Placa == servicio.Vehiculo.Placa)
                {
                    // Actualizar las propiedades del servicio con los nuevos valores
                    servicios[i].EsUrgente = servicio.EsUrgente;
                    servicios[i].Prioridad = servicio.Prioridad;
                    servicios[i].ValorAdicional = servicio.ValorAdicional;
                    break;
                }
            }

            // Guardar los servicios actualizados en el archivo
            using (StreamWriter sw = new StreamWriter(_rutaArchivo))
            {
                foreach (Servicio serv in servicios)
                {
                    sw.WriteLine($"{serv.Cliente.Documento},{serv.Cliente.Nombre},{serv.Cliente.Telefono},{serv.Vehiculo.Placa},{serv.Vehiculo.Marca},{serv.Vehiculo.Modelo},{serv.Vehiculo.Tipo},{serv.TipoServicio},{serv.EsUrgente},{serv.Prioridad},{serv.ValorAdicional}");
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
                        sw.WriteLine($"{servicioActualizado.Cliente.Documento},{servicioActualizado.Cliente.Nombre},{servicioActualizado.Cliente.Telefono},{servicioActualizado.Vehiculo.Placa},{servicioActualizado.Vehiculo.Marca},{servicioActualizado.Vehiculo.Modelo},{servicioActualizado.Vehiculo.Tipo},{servicioActualizado.TipoServicio},{servicioActualizado.EsUrgente},{servicioActualizado.Prioridad},{servicioActualizado.ValorAdicional}");
                    }
                }
            }
        }

        public List<Empleado> ObtenerEmpleadosDisponibles()
        {
            List<Empleado> empleados = new List<Empleado>();

            // Leer el archivo de texto "Empleados.txt" y obtener los empleados disponibles
            string[] lineas = File.ReadAllLines("Empleados.txt");

            foreach (string linea in lineas)
            {
                string[] campos = linea.Split(',');

                int id = int.Parse(campos[0]);
                string nombre = campos[1];
                bool disponible = bool.Parse(campos[2]);

                if (disponible)
                {
                    Empleado empleado = new Empleado(id, nombre, disponible);
                    empleados.Add(empleado);
                }
            }

            return empleados;
        }

        public void ActualizarEmpleado(Empleado empleado)
        {
            string[] lineas = File.ReadAllLines("Empleados.txt");

            for (int i = 0; i < lineas.Length; i++)
            {
                string[] campos = lineas[i].Split(',');

                int id = int.Parse(campos[0]);

                if (id == empleado.Id)
                {
                    lineas[i] = $"{empleado.Id},{empleado.Nombre},{empleado.Disponible}";
                    break;
                }
            }

            File.WriteAllLines("Empleados.txt", lineas);
        }


    }

}


