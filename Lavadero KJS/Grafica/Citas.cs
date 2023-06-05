using Datos;
using Entidades;
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
using static Entidades.Vehiculo;

namespace Grafica
{
    public partial class Citas : Form
    {

        private LogicaCitas logicaCitas;
        //private Cliente clienteSeleccionado;

        public Citas()
        {
            InitializeComponent();
            logicaCitas = new LogicaCitas();
            CargarDatos();
            listBoxCitas.SelectedIndexChanged += ListBoxCitas_SelectedIndexChanged;
        }


        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void btnAgregarCitas_Click(object sender, EventArgs e)
        {
            // Obtener los datos de los controles de la interfaz
            Cliente cliente = new Cliente(txtNombre.Text, txtCedula.Text, txtTelefono.Text);
            Vehiculo.TipoVehiculo tipoVehiculo = ObtenerTipoVehiculoSeleccionado();
            Vehiculo vehiculo = new Vehiculo(txtMarca.Text, txtModelo.Text, txtPlaca.Text, tipoVehiculo);
            string tipoServicio = comboBoxLavado.SelectedItem.ToString();
            DateTime fechaHora = dateTimePicker.Value;

            try
            {
                // Crear una instancia de la lógica de citas
                LogicaCitas logicaCitas = new LogicaCitas();

                // Agregar la cita utilizando la lógica de citas
                logicaCitas.AgregarCita(cliente, vehiculo, tipoServicio, fechaHora);

                // Mostrar un mensaje de éxito
                MessageBox.Show("La cita se ha guardado correctamente.", "Cita Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los controles de la interfaz
                LimpiarControles();

                // Actualizar el listado de citas en el listBoxCitas
                CargarDatos();
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error
                MessageBox.Show("Hubo un problema al guardar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Vehiculo.TipoVehiculo ObtenerTipoVehiculoSeleccionado()
        {
            // Obtener el tipo de vehículo seleccionado en el ComboBox
            string tipoVehiculoSeleccionado = comboBoxTipo.SelectedItem.ToString();

            // Realizar la conversión a Vehiculo.TipoVehiculo
            switch (tipoVehiculoSeleccionado)
            {
                case "Carro":
                    return Vehiculo.TipoVehiculo.Carro;
                case "Moto":
                    return Vehiculo.TipoVehiculo.Moto;
                default:
                    throw new InvalidCastException("El tipo de vehículo seleccionado no es válido.");
            }
        }

        private void LimpiarControles()
        {
            txtNombre.Text = string.Empty;
            txtCedula.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            comboBoxTipo.SelectedIndex = -1;
            comboBoxLavado.SelectedIndex = -1;
            dateTimePicker.Value = DateTime.Now;
        }

        private void CargarDatos()
        {
            listBoxCitas.Items.Clear();

            List<Cita> citas = logicaCitas.ObtenerCitas();

            foreach (Cita cita in citas)
            {
                listBoxCitas.Items.Add(cita.Cliente.Documento);
            }
        }

        private void ListBoxCitas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCitas.SelectedIndex != -1)
            {
                // Obtener la cita seleccionada
                Cita citaSeleccionada = logicaCitas.ObtenerCitas()[listBoxCitas.SelectedIndex];

                // Mostrar los datos de la cita en los controles correspondientes
                txtCNom.Text = citaSeleccionada.Cliente.Nombre;
                txtCCC.Text = citaSeleccionada.Cliente.Documento;
                txtCTel.Text = citaSeleccionada.Cliente.Telefono;
                txtCMar.Text = citaSeleccionada.Vehiculo.Marca;
                txtCMod.Text = citaSeleccionada.Vehiculo.Modelo;
                txtCPla.Text = citaSeleccionada.Vehiculo.Placa;
                cmbTipo.SelectedItem = citaSeleccionada.Vehiculo.Tipo.ToString();
                cmbLavado.SelectedItem = citaSeleccionada.TipoServicio;
                dateTimePicker1.Value = citaSeleccionada.FechaHora;
            }
        }

        private void btnEliminarCitas_Click(object sender, EventArgs e)
        {
            // Verificar si hay una cita seleccionada en el listBoxCitas
            if (listBoxCitas.SelectedItem != null)
            {
                // Obtener el documento del cliente seleccionado en el listBoxCitas
                string documentoCliente = listBoxCitas.SelectedItem.ToString();

                // Buscar la cita correspondiente al documento del cliente en la lista de citas
                Cita citaSeleccionada = logicaCitas.ObtenerCitas().FirstOrDefault(c => c.Cliente.Documento == documentoCliente);

                // Verificar si se encontró la cita seleccionada
                if (citaSeleccionada != null)
                {
                    // Mostrar un cuadro de diálogo de confirmación antes de eliminar la cita
                    DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta cita?", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Verificar la respuesta del usuario
                    if (resultado == DialogResult.Yes)
                    {
                        // Eliminar la cita utilizando el ID de la cita seleccionada
                        logicaCitas.EliminarCita(citaSeleccionada.Id);

                        // Volver a cargar los datos en el listBoxCitas
                        CargarDatos();

                        // Limpiar los controles de datos de la cita eliminada
                        LimpiarControles2();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una cita para eliminar.", "Cita no seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarControles2()
        {
            txtCNom.Text = string.Empty;
            txtCCC.Text = string.Empty;
            txtCTel.Text = string.Empty;
            txtCMar.Text = string.Empty;
            txtCMod.Text = string.Empty;
            txtCPla.Text = string.Empty;
            cmbTipo.SelectedIndex = -1;
            cmbLavado.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
        }

        private void btnEditarCitas_Click(object sender, EventArgs e)
        {
            // Obtener los datos de los controles del formulario
            string nombre = txtCNom.Text;
            string documento = txtCCC.Text;
            string telefono = txtCTel.Text;
            string marca = txtCMar.Text;
            string modelo = txtCMod.Text;
            string placa = txtCPla.Text;
            string tipoServicio = cmbTipo.SelectedItem.ToString();
            DateTime fechaHora = dateTimePicker1.Value;

            // Verificar si se ha seleccionado una cita en el listBoxCitas
            if (listBoxCitas.SelectedIndex != -1)
            {
                // Obtener el documento de la cita seleccionada en el listBoxCitas
                string documentoSeleccionado = listBoxCitas.SelectedItem.ToString();

                // Buscar la cita correspondiente al documento seleccionado
                Cita citaSeleccionada = logicaCitas.ObtenerCitas().FirstOrDefault(c => c.Cliente.Documento == documentoSeleccionado);

                if (citaSeleccionada != null)
                {
                    // Crear una nueva instancia de Cliente con los nuevos datos
                    Cliente cliente = new Cliente(nombre, documento, telefono);

                    // Crear una nueva instancia de Vehiculo con los nuevos datos
                    Vehiculo.TipoVehiculo tipoVehiculo;

                    if (cmbTipo.SelectedItem.ToString() == "Carro")
                    {
                        tipoVehiculo = Vehiculo.TipoVehiculo.Carro;
                    }
                    else if (cmbTipo.SelectedItem.ToString() == "Moto")
                    {
                        tipoVehiculo = Vehiculo.TipoVehiculo.Moto;
                    }
                    else
                    {
                        // Manejar caso de error si no se selecciona un tipo de vehículo válido
                        MessageBox.Show("Seleccione un tipo de vehículo válido (Carro o Moto).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Vehiculo vehiculo = new Vehiculo(marca, modelo, placa, tipoVehiculo);

                    // Crear una nueva instancia de Cita con los nuevos datos
                    Cita citaModificada = new Cita(cliente, vehiculo, tipoServicio, fechaHora, citaSeleccionada.Id);

                    // Modificar la cita llamando al método ModificarCita de LogicaCitas
                    logicaCitas.ModificarCita(citaSeleccionada.Id, cliente, vehiculo, tipoServicio, fechaHora);

                    // Actualizar el listado de citas en el listBoxCitas
                    CargarDatos();
                    MessageBox.Show("La cita ha sido actualizada correctamente.", "Cita Actualizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarControles2();
                }
                else
                {
                    MessageBox.Show("No se encontró la cita seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una cita para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
