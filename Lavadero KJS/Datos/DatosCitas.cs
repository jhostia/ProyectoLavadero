using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosCitas
    {
        private const string NombreArchivo = "citas.txt";

        public static List<Entidades.Cita> ObtenerCitas()
        {
            return LeerCitas();
        }

        public static void AgregarCita(Entidades.Cita cita)
        {
            try
            {
                cita.Id = Guid.NewGuid().ToString(); // Generar un nuevo ID único para la cita
                List<Entidades.Cita> citas = LeerCitas();
                citas.Add(cita);
                GuardarCitas(citas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar la cita: " + ex.Message);
                throw; // Relanzar la excepción para que se propague hacia arriba
            }
        }

        public static void ModificarCita(Entidades.Cita citaModificada)
        {
            List<Entidades.Cita> citas = LeerCitas();

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

        public static void EliminarCita(string idCita)
        {
            List<Entidades.Cita> citas = LeerCitas();
            Entidades.Cita cita = citas.Find(c => c.Id == idCita);

            if (cita != null)
            {
                citas.Remove(cita);
                GuardarCitas(citas);
            }
        }

        private static List<Entidades.Cita> LeerCitas()
        {
            List<Entidades.Cita> citas = new List<Entidades.Cita>();

            if (File.Exists(NombreArchivo))
            {
                string[] lineas = File.ReadAllLines(NombreArchivo);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    if (datos.Length == 10)
                    {
                        string id = datos[0];
                        string nombreCliente = datos[1];
                        string documentoCliente = datos[2];
                        string telefonoCliente = datos[3];
                        string marcaVehiculo = datos[4];
                        string modeloVehiculo = datos[5];
                        string placaVehiculo = datos[6];
                        string tipoVehiculo = datos[7];
                        string tipoServicio = datos[8];
                        DateTime fechaHora = DateTime.Parse(datos[9]);

                        Entidades.Cliente cliente = new Entidades.Cliente(nombreCliente, documentoCliente, telefonoCliente);
                        Entidades.Vehiculo vehiculo = new Entidades.Vehiculo(marcaVehiculo, modeloVehiculo, placaVehiculo, (Entidades.Vehiculo.TipoVehiculo)Enum.Parse(typeof(Entidades.Vehiculo.TipoVehiculo), tipoVehiculo));
                        Entidades.Cita cita = new Entidades.Cita(cliente, vehiculo, tipoServicio, fechaHora, id);

                        citas.Add(cita);
                    }
                }
            }

            return citas;
        }

        private static void GuardarCitas(List<Entidades.Cita> citas)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(NombreArchivo))
                {
                    foreach (Entidades.Cita cita in citas)
                    {
                        string linea = $"{cita.Id},{cita.Cliente.Nombre},{cita.Cliente.Documento},{cita.Cliente.Telefono},{cita.Vehiculo.Marca}," +
                            $"{cita.Vehiculo.Modelo},{cita.Vehiculo.Placa},{cita.Vehiculo.Tipo},{cita.TipoServicio},{cita.FechaHora}";

                        writer.WriteLine(linea);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar las citas: " + ex.Message);
                throw; // Relanzar la excepción para que se propague hacia arriba
            }
        }
    }
}
