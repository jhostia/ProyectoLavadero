using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }

        public Cliente(string nombre, string documento, string telefono)
        {
            Nombre = nombre;
            Documento = documento;
            Telefono = telefono;
        }
    }
}
