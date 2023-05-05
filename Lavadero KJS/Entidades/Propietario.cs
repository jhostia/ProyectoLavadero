using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Propietario
    {
        public Propietario()
        {
        }

        public Propietario(int id, string nombre, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
        }

        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Telefono { get; set; }
    }
}
