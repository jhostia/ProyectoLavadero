using Entidades;
using Datos;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica
{
    public partial class VentanaEmpleados : Form
    {
        public VentanaEmpleados()
        {
            InitializeComponent();
            CargarEmpleados();
        }
        private void CargarEmpleados()
        {
            listBoxEmpleados.Items.Clear();

            LogicaEmpleados logicaEmpleado = new LogicaEmpleados();
            List<Empleado> empleados = logicaEmpleado.ObtenerEmpleados();

            foreach (Empleado empleado in empleados)
            {
                listBoxEmpleados.Items.Add(empleado.Id);
            }
        }

        private void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            //Se leen todos los campos 
            int Id = int.Parse(txtId.Text.Trim());
            string Nombre = txtNombre.Text.Trim(); //El trim es para eliminar los espacios en blanco 
            string Apellido = txtApellido.Text.Trim();
            string Telefono = txtTelefono.Text.Trim();

            // Crear un nuevo objeto Empleado y asignar los valores
            Empleado empleado = new Empleado
            {
                Id = Id,
                Nombre = Nombre,
                Apellido = Apellido,
                Telefono = Telefono
            };

            LogicaEmpleados logicaEmpleado = new LogicaEmpleados();
            logicaEmpleado.GuardarEmpleado(empleado);

            limpiarCasillas();

            MessageBox.Show("Empleado registrado exitosamente.");
        }
        private void limpiarCasillas()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
        }
        private void VentanaEmpleados_lod(object sender, EventArgs e)
        {
            // Cargar los servicios al cargar la ventana
            CargarEmpleados();
        }
        private void tabControl1_MouseClick_1(object sender, MouseEventArgs e)
        {
            CargarEmpleados();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            // Obtener id del empleado seleccionado en el ListBox
            int Id = int.Parse(listBoxEmpleados.SelectedItem.ToString());

            // Obtener el servicio correspondiente a la placa seleccionada
            LogicaEmpleados logicaEmpleados = new LogicaEmpleados();
            Empleado empleado = logicaEmpleados.ObtenerEmpleadoPorId(Id);

            // Cargar los datos del servicio en los TextBox

            empleado.Id = int.Parse(txtCc.Text);
            empleado.Nombre = txtNom.Text;
            empleado.Apellido = txtApe.Text;
            empleado.Telefono = txtTel.Text;


            // Actualizar el servicio en la capa lógica
            logicaEmpleados.ActualizarEmpleado(empleado);

            // Actualizar el ListBox y los TextBox/ComboBox
            CargarEmpleados();
            limpiarCasillas();
        }

        private void btbEliminar_Click_1(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un servicio en el ListBox
            if (listBoxEmpleados.SelectedItem != null)
            {
                // Obtener la placa del vehículo seleccionado en el ListBox
                int id = int.Parse(listBoxEmpleados.SelectedItem.ToString());

                // Obtener el servicio correspondiente a la placa seleccionada
                LogicaEmpleados logicaEmpleado = new LogicaEmpleados();
                Empleado empleado = logicaEmpleado.ObtenerEmpleadoPorId(id);

                // Eliminar el servicio de la capa lógica
                logicaEmpleado.EliminarEmpleado(empleado);

                // Actualizar el ListBox y los TextBox/ComboBox

                txtCc.Clear();
                txtNom.Clear();
                txtApe.Clear(); 
                txtTel.Clear();
                CargarEmpleados();
                limpiarCasillas();
            }
        }

        private void listBoxEmpleados_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Obtener la el Id de la persona seleccionado en el ListBox
            int Id = int.Parse(listBoxEmpleados.SelectedItem.ToString());

            // Obtener la informacion correspondiente al Id seleccionado
            LogicaEmpleados logicaEmpleados = new LogicaEmpleados();
            Empleado empleado = logicaEmpleados.ObtenerEmpleadoPorId(Id);

            // Cargar los datos del servicio en los TextBox
            txtCc.Text = empleado.Id.ToString();
            txtNom.Text = empleado.Nombre;
            txtApe.Text = empleado.Apellido;
            txtTel.Text = empleado.Telefono;
        }
    }
}
