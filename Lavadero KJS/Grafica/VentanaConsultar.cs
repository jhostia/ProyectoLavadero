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
using Microsoft.TeamFoundation.TestManagement.WebApi;

namespace Presentacion
{
    public partial class VentanaConsultar : Form
    {
        public VentanaConsultar()
        {
            InitializeComponent();
            CargarServicios();
        }

        private void CargarServicios()
        {
            listBoxServicios.Items.Clear();

            LogicaLavadero logicaLavadero = new LogicaLavadero();
            List<Servicio> servicios = logicaLavadero.ObtenerServicios();

            foreach (Servicio servicio in servicios)
            {
                listBoxServicios.Items.Add(servicio.Vehiculo.Placa);
            }
        }


        private void VentanaConsultar_Load(object sender, EventArgs e)
        {
            // Cargar los servicios al cargar la ventana
            CargarServicios();
        }

        private void listBoxServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener la placa del vehículo seleccionado en el ListBox
            string placa = listBoxServicios.SelectedItem.ToString();

            // Obtener el servicio correspondiente a la placa seleccionada
            LogicaLavadero logicaLavadero = new LogicaLavadero();
            Servicio servicio = logicaLavadero.ObtenerServicioPorPlaca(placa);

            // Cargar los datos del servicio en los TextBox y ComboBox
            txtCCliente.Text = servicio.Cliente.Nombre;
            txtCDocumento.Text = servicio.Cliente.Documento;
            txtCNumero.Text = servicio.Cliente.Telefono;
            txtCMarca.Text = servicio.Vehiculo.Marca;
            txtCModelo.Text = servicio.Vehiculo.Modelo;
            txtCPlaca.Text = servicio.Vehiculo.Placa;
            comboBoxCTipo.SelectedItem = servicio.Vehiculo.Tipo.ToString();
            comboBoxCLavado.SelectedItem = servicio.TipoServicio;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Obtener la placa del vehículo seleccionado en el ListBox
            string placa = listBoxServicios.SelectedItem.ToString();

            // Obtener el servicio correspondiente a la placa seleccionada
            LogicaLavadero logicaLavadero = new LogicaLavadero();
            Servicio servicio = logicaLavadero.ObtenerServicioPorPlaca(placa);

            // Actualizar los datos del servicio con los valores ingresados en los TextBox y ComboBox
            servicio.Cliente.Nombre = txtCCliente.Text;
            servicio.Cliente.Documento = txtCDocumento.Text;
            servicio.Cliente.Telefono = txtCNumero.Text;
            servicio.Vehiculo.Marca = txtCMarca.Text;
            servicio.Vehiculo.Modelo = txtCModelo.Text;
            servicio.Vehiculo.Placa = txtCPlaca.Text;
            servicio.Vehiculo.Tipo = (Vehiculo.TipoVehiculo)Enum.Parse(typeof(Vehiculo.TipoVehiculo), comboBoxCTipo.SelectedItem.ToString());
            servicio.TipoServicio = comboBoxCLavado.SelectedItem.ToString();

            // Actualizar el servicio en la capa lógica
            logicaLavadero.ActualizarServicio(servicio);

            // Actualizar el ListBox y los TextBox/ComboBox
            CargarServicios();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtCCliente.Text = string.Empty;
            txtCDocumento.Text = string.Empty;
            txtCNumero.Text = string.Empty;
            txtCMarca.Text = string.Empty;
            txtCModelo.Text = string.Empty;
            txtCPlaca.Text = string.Empty;
            comboBoxCTipo.SelectedIndex = -1;
            comboBoxCLavado.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un servicio en el ListBox
            if (listBoxServicios.SelectedItem != null)
            {
                // Obtener la placa del vehículo seleccionado en el ListBox
                string placa = listBoxServicios.SelectedItem.ToString();

                // Obtener el servicio correspondiente a la placa seleccionada
                LogicaLavadero logicaLavadero = new LogicaLavadero();
                Servicio servicio = logicaLavadero.ObtenerServicioPorPlaca(placa);

                // Eliminar el servicio de la capa lógica
                logicaLavadero.EliminarServicio(servicio);

                // Actualizar el ListBox y los TextBox/ComboBox
                CargarServicios();
                LimpiarCampos();
            }
        }
    }
}
