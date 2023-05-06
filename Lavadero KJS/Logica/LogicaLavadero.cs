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

            _datosServicios.AgregarServicio(servicio);
        }

        public List<Servicio> ObtenerServicios()
        {
            return _datosServicios.ObtenerServicios();
        }
    }
}
