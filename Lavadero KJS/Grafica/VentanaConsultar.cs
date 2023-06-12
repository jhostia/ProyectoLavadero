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
        private List<Servicio> servicios;
        private Servicio servicioSeleccionado; // Variable miembro para almacenar el servicio seleccionado

        public VentanaConsultar()
        {
            InitializeComponent();
            CargarServicios();
        }

        private void CargarServicios()
        {
            listBoxServicios.Items.Clear();

            LogicaLavadero logicaLavadero = new LogicaLavadero();
            servicios = logicaLavadero.ObtenerServicios();

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
            if (listBoxServicios.SelectedItem != null)
            {
                // Obtener la placa del vehículo seleccionado en el ListBox
                string placa = listBoxServicios.SelectedItem.ToString();
                // Obtener el servicio correspondiente a la placa seleccionada
                LogicaLavadero logicaLavadero = new LogicaLavadero();
                Servicio servicio = logicaLavadero.ObtenerServicioPorPlaca(placa);

                // Asignar el servicio seleccionado a una variable miembro de la clase VentanaConsultar
                servicioSeleccionado = servicio;

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

        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un servicio en el ListBox
            if (listBoxServicios.SelectedItem != null)
            {
                // Obtener la placa del vehículo seleccionado en el ListBox
                string placa = listBoxServicios.SelectedItem.ToString();

                // Obtener el servicio correspondiente a la placa seleccionada
                Servicio servicio = servicios.Find(s => s.Vehiculo.Placa == placa);

                if (servicio != null)
                {
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
                    LogicaLavadero logicaLavadero = new LogicaLavadero();
                    logicaLavadero.ActualizarServicio(servicio);

                    // Actualizar el ListBox y los TextBox/ComboBox
                    CargarServicios();
                    LimpiarCampos();
                }
            }
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
                Servicio servicio = servicios.Find(s => s.Vehiculo.Placa == placa);

                if (servicio != null)
                {
                    // Eliminar el servicio de la capa lógica
                    LogicaLavadero logicaLavadero = new LogicaLavadero();
                    logicaLavadero.EliminarServicio(servicio);

                    // Actualizar el ListBox y los TextBox/ComboBox
                    CargarServicios();
                    LimpiarCampos();
                }
            }
        }

        // Evento Click del botón de generar factura
        private void btnFactura_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un servicio válido
            if (listBoxServicios.SelectedItem is string placa)
            {
                // Obtener el servicio correspondiente a la placa seleccionada
                LogicaLavadero logicaLavadero = new LogicaLavadero();
                Servicio servicio = logicaLavadero.ObtenerServicioPorPlaca(placa);

                // Obtener los datos del servicio seleccionado
                string nombreCliente = servicio.Cliente.Nombre;
                string documentoCliente = servicio.Cliente.Documento;
                string marcaVehiculo = servicio.Vehiculo.Marca;
                string modeloVehiculo = servicio.Vehiculo.Modelo;
                string placaVehiculo = servicio.Vehiculo.Placa;
                string tipoServicio = servicio.TipoServicio;
                decimal costoServicio = servicio.Costo;
                decimal valorAdicional = servicio.ValorAdicional;
                decimal costoTotal = servicio.CalcularValorTotal();
                string fechaServicio = servicio.ObtenerFechaServicio();
                string horaServicio = servicio.ObtenerHoraServicio();

                // Generar la cadena de la factura
                StringBuilder sb = new StringBuilder();

                // Encabezado de la factura
                sb.AppendLine("Nombre del Lavadero");
                sb.AppendLine("Dirección del Lavadero");
                sb.AppendLine("Teléfono del Lavadero");
                sb.AppendLine();

                // Datos del servicio
                sb.AppendLine("Datos del servicio.");
                sb.AppendLine($"Fecha: {fechaServicio}");

                string horaFormateada = horaServicio.Substring(0, horaServicio.LastIndexOf(" ")) + " " + horaServicio.Substring(horaServicio.LastIndexOf(" ") + 1);
                sb.AppendLine($"Hora: {horaFormateada}");

                sb.AppendLine($"Tipo de servicio: {tipoServicio}");
                sb.AppendLine($"Costo del servicio: {costoServicio:C}");
                sb.AppendLine($"Valor adicional: {valorAdicional:C}");
                sb.AppendLine($"Costo total: {costoTotal:C}");
                sb.AppendLine();

                // Datos del vehiculo
                sb.AppendLine("Datos del vehiculo.");
                sb.AppendLine($"Marca: {marcaVehiculo}");
                sb.AppendLine($"Modelo: {modeloVehiculo}");
                sb.AppendLine($"Placa: {placaVehiculo}");
                sb.AppendLine();

                // Datos del propietario
                sb.AppendLine("Datos del propietario.");
                sb.AppendLine($"Nombre: {nombreCliente}");
                sb.AppendLine($"Documento: {documentoCliente}");
                sb.AppendLine();

                // Mensaje final
                sb.AppendLine("Mensaje de la factura");

                // Mostrar la factura en una ventana emergente
                VentanaFactura ventanaFactura = new VentanaFactura(sb.ToString());
                ventanaFactura.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione un servicio válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







    }
}

