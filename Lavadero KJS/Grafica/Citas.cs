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

namespace Grafica
{
    public partial class Citas : Form
    {

        private LogicaCitas logicaCitas;
        private Cliente clienteSeleccionado;
        public Citas()
        {
            InitializeComponent();
            ActualizarListBoxCitas();
           
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
            string tipoServicio = comboBoxTipo.SelectedItem.ToString();
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

        private void listBoxCitas_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarDetallesCitaSeleccionada();
        }

        private void MostrarDetallesCitaSeleccionada()
        {
            if (listBoxCitas.SelectedItem != null)
            {
                Cita citaSeleccionada = (Cita)listBoxCitas.SelectedItem;

                // Mostrar los detalles de la cita en los controles correspondientes
                txtConsultarNom.Text = citaSeleccionada.Cliente.Nombre;
                txtConsultarCC.Text = citaSeleccionada.Cliente.Documento;
                txtConsultarTel.Text = citaSeleccionada.Cliente.Telefono;
                txtConsultarPlaca.Text = citaSeleccionada.Vehiculo.Placa;
                // Mostrar la fecha agendada en un control apropiado, como un DateTimePicker o un TextBox
                // Aquí puedes elegir el control que mejor se ajuste a tus necesidades

                // Opcional: También puedes mostrar otros detalles de la cita si los necesitas
            }
        }

        private void btnEliminarCitas_Click(object sender, EventArgs e)
        {
            // Obtener la cita seleccionada en el ListBox
            Cita citaSeleccionada = listBoxCitas.SelectedItem as Cita;

            if (citaSeleccionada != null)
            {
                try
                {
                    // Eliminar la cita utilizando la lógica de citas
                    LogicaCitas logicaCitas = new LogicaCitas();
                    logicaCitas.EliminarCita(citaSeleccionada.Id);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("La cita se ha eliminado correctamente.", "Cita Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar el ListBox de citas
                    ActualizarListBoxCitas();

                    // Limpiar los controles de la interfaz
                    LimpiarControles();
                }
                catch (Exception ex)
                {
                    // Mostrar un mensaje de error
                    MessageBox.Show("Hubo un problema al eliminar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnEditarCitas_Click(object sender, EventArgs e)
        {
            // Obtener la cita seleccionada en el ListBox
            Cita citaSeleccionada = listBoxCitas.SelectedItem as Cita;

            if (citaSeleccionada != null)
            {
                try
                {
                    // Obtener los datos de los controles de la interfaz
                    Cliente cliente = new Cliente(txtNombre.Text, txtCedula.Text, txtTelefono.Text);
                    Vehiculo.TipoVehiculo tipoVehiculo = ObtenerTipoVehiculoSeleccionado();
                    Vehiculo vehiculo = new Vehiculo(txtMarca.Text, txtModelo.Text, txtPlaca.Text, tipoVehiculo);
                    string tipoServicio = comboBoxTipo.SelectedItem.ToString();
                    DateTime fechaHora = dateTimePicker.Value;

                    // Crear una instancia de la lógica de citas
                    LogicaCitas logicaCitas = new LogicaCitas();

                    // Modificar la cita utilizando la lógica de citas
                    logicaCitas.ModificarCita(citaSeleccionada.Id, cliente, vehiculo, tipoServicio, fechaHora);

                    // Mostrar un mensaje de éxito
                    MessageBox.Show("La cita se ha modificado correctamente.", "Cita Modificada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar el ListBox de citas
                    ActualizarListBoxCitas();

                    // Limpiar los controles de la interfaz
                    LimpiarControles();
                }
                catch (Exception ex)
                {
                    // Mostrar un mensaje de error
                    MessageBox.Show("Hubo un problema al modificar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void EliminarCita(Cita cita)
        {
            try
            {
                // Crear una instancia de la lógica de citas
                LogicaCitas logicaCitas = new LogicaCitas();

                // Llamar al método para eliminar la cita
                logicaCitas.EliminarCita(cita.Id);

                // Mostrar un mensaje de éxito
                MessageBox.Show("La cita se ha eliminado correctamente.", "Cita Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los controles de la interfaz
                LimpiarControles();
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error
                MessageBox.Show("Hubo un problema al eliminar la cita: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ActualizarListBoxCitas()
        {
            // Limpiar el ListBox antes de cargar las citas
            listBoxCitas.Items.Clear();

            // Obtener la lista de citas desde la capa lógica de citas
            List<Cita> citas = logicaCitas.ObtenerCitas();

            // Recorrer la lista de citas y agregar las cédulas de los clientes al ListBox
            foreach (Cita cita in citas)
            {
                listBoxCitas.Items.Add(cita.Cliente.Documento);
            }
        }



    }
}
