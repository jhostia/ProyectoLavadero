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


namespace Grafica
{
    public partial class VentanaInicio : Form
    {
        private DatosServicios datosServicios; // Declarar la instancia de DatosServicios

        public VentanaInicio()
        {
            InitializeComponent();

            // Inicializar la instancia de DatosServicios
            datosServicios = new DatosServicios();
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            pnlIniciar.Visible = false; // Ocultar el panel de inicio de sesión
            pnlRegistrar.Visible = true; // Mostrar el panel de registro

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos ingresados por el usuario en la interfaz
                string nombreUsuario = txtUsuario.Text;
                string contrasena = txtContraseña.Text;

                // Iniciar sesión usando la capa de lógica
                LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
                Usuario usuario = logicaUsuarios.IniciarSesion(nombreUsuario, contrasena);

                MessageBox.Show($"Bienvenido, {usuario.NombreUsuario}.");
                pnlDatos.Visible = true;
                pnlIniciar.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
            }
        }

        

        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Crear un objeto Usuario con los datos ingresados por el usuario en la interfaz
                Usuario usuario = new Usuario
                {
                    NombreUsuario = txtReUsuario.Text,
                    CorreoElectronico = txtReCorreo.Text,
                    Contrasena = txtReContra.Text
                };

                // Guardar el usuario usando la capa de lógica
                LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
                logicaUsuarios.GuardarUsuario(usuario);

                MessageBox.Show("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}");
            }

            pnlRegistrar.Visible = false;
            pnlIniciar.Visible = true;
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
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

            // Agregar el servicio a la lista de servicios
            datosServicios.AgregarServicio(servicio);

            // Crear una nueva instancia de Factura y mostrarla en una ventana nueva
            Factura factura = datosServicios.GenerarFactura();
            VentanaFactura ventanaFactura = new VentanaFactura(factura.ToString());
            ventanaFactura.Show();

            MessageBox.Show("Servicio agregado correctamente.");
        }
    }
}
