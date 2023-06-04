using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class LogicaLavadero
    {
        private readonly DatosServicios _datosServicios;

        public LogicaLavadero()
        {
            _datosServicios = new DatosServicios();
        }

        public void GuardarServicio(Servicio servicio)
        {
            if (string.IsNullOrEmpty(servicio.Vehiculo.Placa) || string.IsNullOrEmpty(servicio.Vehiculo.Marca) || string.IsNullOrEmpty(servicio.Vehiculo.Modelo)
                || string.IsNullOrEmpty(servicio.Cliente.Nombre) || string.IsNullOrEmpty(servicio.Cliente.Documento) || string.IsNullOrEmpty(servicio.Cliente.Telefono))
            {
                throw new ArgumentException("Por favor complete todos los campos.");
            }

            if (servicio.Prioridad == 5)
            {
                servicio.ValorAdicional = 4000; // Aplicar valor adicional para prioridad del cliente
            }

            _datosServicios.AgregarServicio(servicio);
        }

        public List<Servicio> ObtenerServicios()
        {
            return _datosServicios.ObtenerServicios();
        }

        public Servicio ObtenerServicioPorPlaca(string placa)
        {
            // Iterar sobre la lista de servicios y buscar el servicio con la placa especificada
            foreach (Servicio servicio in _datosServicios.ObtenerServicios())
            {
                if (servicio.Vehiculo.Placa == placa)
                {
                    return servicio;
                }
            }

            // Si no se encuentra ningún servicio con la placa especificada, retornar null o lanzar una excepción
            return null;
        }

        public void ActualizarServicio(Servicio servicio)
        {
            if (string.IsNullOrEmpty(servicio.Vehiculo.Placa) || string.IsNullOrEmpty(servicio.Vehiculo.Marca) || string.IsNullOrEmpty(servicio.Vehiculo.Modelo)
                || string.IsNullOrEmpty(servicio.Cliente.Nombre) || string.IsNullOrEmpty(servicio.Cliente.Documento) || string.IsNullOrEmpty(servicio.Cliente.Telefono))
            {
                throw new ArgumentException("Por favor complete todos los campos.");
            }

            if (servicio.Prioridad == 5)
            {
                servicio.ValorAdicional = 4000; // Actualizar el valor adicional para prioridad del cliente
            }

            _datosServicios.ActualizarServicio(servicio);
        }

        public void EliminarServicio(Servicio servicio)
        {
            _datosServicios.EliminarServicio(servicio);
        }
    }

}

