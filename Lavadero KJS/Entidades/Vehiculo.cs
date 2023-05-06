using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public TipoVehiculo Tipo { get; set; }
        public TipoServicio Servicio { get; set; }

        public Vehiculo(string marca, string modelo, string placa, TipoVehiculo tipoVehiculo)
        {
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            Tipo = tipoVehiculo;
        }

        public enum TipoVehiculo
        {
            Carro,
            Moto
        }

        public enum TipoServicio
        {
            Enjuague,
            LavadoCompleto
        }
    }
}
