using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        //public float Salario { get; set; }

        public Empleado() { }

        public Empleado(int id, string nombre, string apellido, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            //Salario = salario;  
        }
        public override string ToString()
        {
            return $"{Id};{Nombre};{Apellido};{Telefono}";
        }
    }
}
