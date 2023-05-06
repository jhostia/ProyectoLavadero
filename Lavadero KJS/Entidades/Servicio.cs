using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Servicio
    {
        public Servicio(Cliente cliente, Vehiculo vehiculo, string tipoVehiculo, string tipoServicio)
        {
            Cliente = cliente;
            Vehiculo = vehiculo;
            TipoVehiculo = tipoVehiculo;
            TipoServicio = tipoServicio;
        }

        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoServicio { get; set; }
        public decimal Costo => CalcularCosto();

        public decimal CalcularCosto()
        {
            decimal costoLavado, costoEnjuague;
            if (TipoVehiculo == Enum.GetName(typeof(Vehiculo.TipoVehiculo), Vehiculo.TipoVehiculo.Moto))
            {
                costoLavado = 7000m;
                costoEnjuague = 5000m;
            }
            else
            {
                costoLavado = 25000m;
                costoEnjuague = 15000m;
            }

            decimal costoTotal = 0m;
            if (TipoServicio == "Lavado completo")
            {
                costoTotal += costoLavado;
            }
            if (TipoServicio == "Enjuague")
            {
                costoTotal += costoEnjuague;
            }

            return costoTotal;
        }

    }
}

