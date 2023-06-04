using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Servicio
    {
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoServicio { get; set; }
        public bool EsUrgente { get; set; }
        public int Prioridad { get; set; }
        public int ValorAdicional { get; set; }
        public decimal Costo { get; set; }
        public decimal ValorServicio { get; set; }
        public decimal ValorTotal => CalcularValorTotal();
        public DateTime FechaServicio { get; set; } // Nueva propiedad para la fecha del servicio

        public Servicio(Cliente cliente, Vehiculo vehiculo, string tipoVehiculo, string tipoServicio)
        {
            Cliente = cliente;
            Vehiculo = vehiculo;
            TipoVehiculo = tipoVehiculo;
            TipoServicio = tipoServicio;
            EsUrgente = false;
            Prioridad = 0;
            ValorAdicional = 0;
            Costo = CalcularCosto();
            ValorServicio = CalcularCosto();
            FechaServicio = DateTime.Now; // Asignar la fecha actual al crear el servicio
        }

        public decimal CalcularCosto()
        {
            decimal costo = 0;

            if (TipoServicio == "Enjuague")
            {
                if (Vehiculo.Tipo == Vehiculo.TipoVehiculo.Moto)
                {
                    costo = 5000;
                }
                else if (Vehiculo.Tipo == Vehiculo.TipoVehiculo.Carro)
                {
                    costo = 15000;
                }
            }
            else if (TipoServicio == "Lavado completo")
            {
                if (Vehiculo.Tipo == Vehiculo.TipoVehiculo.Moto)
                {
                    costo = 8000;
                }
                else if (Vehiculo.Tipo == Vehiculo.TipoVehiculo.Carro)
                {
                    costo = 25000;
                }
            }

            return costo;
        }


        public override string ToString()
        {
            string urgente = EsUrgente ? "Sí" : "No";

            return $"{Cliente.Nombre},{Cliente.Documento},{Cliente.Telefono},{Vehiculo.Marca},{Vehiculo.Modelo},{Vehiculo.Placa}," +
                   $"{TipoVehiculo},{TipoServicio},{EsUrgente},{Prioridad},{ValorAdicional}";
        }

        public decimal CalcularValorTotal()
        {
            return ValorServicio + ValorAdicional;
        }

        public string ObtenerFechaServicio()
        {
            return FechaServicio.ToString("dd/MM/yyyy");
        }

        public string ObtenerHoraServicio()
        {
            return FechaServicio.ToString("hh:mm tt");
        }
    }
}



