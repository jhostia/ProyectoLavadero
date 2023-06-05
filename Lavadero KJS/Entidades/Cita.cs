using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cita
    {
        public Cliente Cliente { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public string TipoServicio { get; set; }
        public DateTime FechaHora { get; set; }
        public string Id { get; set; }

        public Cita(Cliente cliente, Vehiculo vehiculo, string tipoServicio, DateTime fechaHora, string id)
        {
            Cliente = cliente;
            Vehiculo = vehiculo;
            TipoServicio = tipoServicio;
            FechaHora = fechaHora;
            Id = id;
        }
    }
}
    
