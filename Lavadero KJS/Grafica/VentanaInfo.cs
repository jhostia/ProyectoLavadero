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
            Cliente cliente = new Cliente(nombreCliente, documentoCliente, telefonoCliente);
            Vehiculo.TipoVehiculo tipodeVehiculo = (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), comboBoxTipo.Text);
            Vehiculo vehiculo = new Vehiculo(txtMarca.Text, txtModelo.Text, txtPlaca.Text, tipodeVehiculo);
            Servicio servicio = new Servicio(cliente, vehiculo, tipoVehiculo, tipoServicio);

            // Verificar si es un servicio urgente
            bool esUrgente = radioButtonSi.Checked;
            servicio.EsUrgente = esUrgente;

            // Si es urgente, obtener la prioridad seleccionada del ComboBox
            if (esUrgente)
            {
                int prioridad = comboBoxUrgente.SelectedIndex + 1;
                servicio.Prioridad = prioridad;

                // Obtener el valor adicional según la prioridad seleccionada
                int valorAdicional = ObtenerValorAdicional(prioridad);
                servicio.ValorAdicional = valorAdicional;

                // Sumar el valor adicional al costo del servicio
                servicio.Costo += valorAdicional;
            }
            else
            {
                servicio.Prioridad = 0; // Prioridad no aplicable
                servicio.ValorAdicional = 0;
            }

            // Agregar el servicio a la lista de servicios
            datosServicios.AgregarServicio(servicio);

            // Crear una nueva instancia de Factura y mostrarla en una ventana nueva
            Factura factura = datosServicios.GenerarFactura(servicio);
            VentanaFactura ventanaFactura = new VentanaFactura(factura.ToString());
            ventanaFactura.Show();

            MessageBox.Show("Servicio agregado correctamente.");
            
            LimpiarCampos();
        }

        private int ObtenerValorAdicional(int prioridad)
        {
            int valorAdicional = 0;
            switch (prioridad)
            {
                case 5:
                    valorAdicional = 4000;
                    break;
                    // Agregar más casos para las demás prioridades si es necesario
            }
            return valorAdicional;
        }

        private void LimpiarCampos()
        {
            // Limpiar los campos de entrada de datos
            txtCliente.Clear();
            txtDocumento.Clear();
            txtNumero.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtPlaca.Clear();
            comboBoxTipo.SelectedIndex = -1;
            comboBoxLavado.SelectedIndex = -1;
            radioButtonSi.Checked = false;
            radioButtonNo.Checked = false;
            comboBoxUrgente.Visible = false;
            comboBoxUrgente.SelectedIndex = -1;
        }

        private void radioButtonSi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSi.Checked)
            {
                comboBoxUrgente.Visible = true;
            }
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNo.Checked)
            {
                comboBoxUrgente.Visible = false;
            }
        }
    }
}
