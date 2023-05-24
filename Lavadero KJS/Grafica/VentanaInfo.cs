using Entidades;
using Datos;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class VentanaInfo : Form
    {
        private DatosServicios datosServicios; // Declarar la instancia de DatosServicios
        public VentanaInfo()
        {
            InitializeComponent();
            // Inicializar la instancia de DatosServicios
            datosServicios = new DatosServicios();
        }

        private void btnAgregarServicio_Click_1(object sender, EventArgs e)
        {
            // Obtener los datos del servicio
            string nombreCliente = txtCliente.Text;
            string documentoCliente = txtDocumento.Text;
            string telefonoCliente = txtNumero.Text;
            string marcaVehiculo = txtMarca.Text;
            string modeloVehiculo = txtModelo.Text;
            string placaVehiculo = txtPlaca.Text;
            string tipoVehiculo = comboBoxTipo.SelectedItem.ToString();
            string tipoServicio = comboBoxLavado.SelectedItem.ToString();

            // Crear objetos de cliente, vehículo y servicio
            Cliente cliente = new Cliente(documentoCliente, nombreCliente, telefonoCliente);
            Vehiculo.TipoVehiculo tipodeVehiculo = (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), comboBoxTipo.Text);
            Vehiculo vehiculo = new Vehiculo(txtMarca.Text, txtModelo.Text, txtPlaca.Text, tipodeVehiculo);
            Servicio servicio = new Servicio(cliente, vehiculo, tipoVehiculo, tipoServicio);

            //Agregar el servicio a la lista de servicios
            datosServicios.AgregarServicio(servicio);

             //Crear una nueva instancia de Factura y mostrarla en una ventana nueva
            Factura factura = datosServicios.GenerarFactura();
            VentanaFactura ventanaFactura = new VentanaFactura(factura.ToString());
            ventanaFactura.Show();

            MessageBox.Show("Servicio agregado correctamente.");

            txtCliente.Clear();
            txtDocumento.Clear();
            txtNumero.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtPlaca.Clear();
            comboBoxTipo.Items.Clear();
            comboBoxLavado.Items.Clear();

        }
    }
}
