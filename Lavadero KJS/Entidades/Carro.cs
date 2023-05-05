using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidades
{
    public class Carro : Propietario
    {
        public Carro()
        {
        }

        public Carro(int id, string nombre, string telefono, string marca, string modelo, string placa) : base(id, nombre, telefono)
        {
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
        }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public override string ToString()
        {
            return $"{Id}; {Nombre}; {Telefono}; {Marca}; {Modelo}; {Placa}";
        }
    }
}
