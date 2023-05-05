
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidades
{   public class Moto : Propietario
    {
        public Moto()
        {
        }

        public Moto(int id, string nombre, string telefono, string marcaMoto, string modeloMoto, string placaMoto) : base(id, nombre, telefono)
        {
            MarcaMoto = marcaMoto;
            ModeloMoto = modeloMoto;
            PlacaMoto = placaMoto;
        }

        public string MarcaMoto { get; set; }
        public string ModeloMoto { get; set; }
        public string PlacaMoto { get; set; }

        public override string ToString()
        {
            return $"{Id}; {Nombre}; {Telefono}; {MarcaMoto}; {ModeloMoto}; {PlacaMoto}";
        }
    }
}
