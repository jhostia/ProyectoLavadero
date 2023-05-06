﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Factura
    {
        public string Lavadero { get; set; }
        public DateTime Fecha { get; set; }
        public Servicio Servicio { get; set; }
        public string NombreCliente { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }

        public Factura(string lavadero, DateTime fecha, Servicio servicio, string nombreCliente, string direccion, string telefono, string mensaje)
        {
            Lavadero = lavadero;
            Fecha = fecha;
            Servicio = servicio;
            NombreCliente = nombreCliente;
            Direccion = direccion;
            Telefono = telefono;
            Mensaje = mensaje;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Encabezado de la factura
            sb.AppendLine($"{Lavadero}");
            sb.AppendLine($"Direccion: {Direccion}");
            sb.AppendLine($"Telefono: {Telefono}");
            sb.AppendLine();

            // Datos del servicio
            sb.AppendLine("Datos del servicio.");
            sb.AppendLine($"Fecha: {Fecha.ToShortDateString()}");
            sb.AppendLine($"Hora: {Fecha.ToShortTimeString()}");
            sb.AppendLine($"Tipo de servicio: {Servicio.TipoServicio}");
            sb.AppendLine($"Costo: {Servicio.Costo:C}");

            sb.AppendLine();

            // Datos del vehiculo
            sb.AppendLine("Datos del vehiculo.");
            sb.AppendLine($"Marca: {Servicio.Vehiculo.Marca}");
            sb.AppendLine($"Modelo: {Servicio.Vehiculo.Modelo}");
            sb.AppendLine($"Placa: {Servicio.Vehiculo.Placa}");
            sb.AppendLine();

            // Datos del propietario
            sb.AppendLine("Datos del propietario.");
            sb.AppendLine($"Nombre: {Servicio.Cliente.Nombre}");
            sb.AppendLine($"Documento: {Servicio.Cliente.Documento}");
            sb.AppendLine();

            // Mensaje final
            sb.AppendLine(Mensaje);

            return sb.ToString();
        }

    }
}
