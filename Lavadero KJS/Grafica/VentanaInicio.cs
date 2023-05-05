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
        public VentanaInicio()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Form ventana = new Registrar();
            ventana.Show();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Leer los datos ingresados en los campos de texto
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtContraseña.Text.Trim();

            // Validar que los campos no estén vacíos
            if (nombreUsuario == "" || contrasena == "")
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Abrir el archivo de texto donde se guardan los datos de los usuarios
            string rutaArchivo = "usuarios.txt";

            // Verificar si el usuario y la contraseña coinciden con los datos almacenados en el archivo de texto
            bool usuarioEncontrado = false;

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                while (!sr.EndOfStream)
                {
                    string linea = sr.ReadLine();
                    string[] datosUsuario = linea.Split(',');

                    if (datosUsuario[0] == nombreUsuario && datosUsuario[2] == contrasena)
                    {
                        usuarioEncontrado = true;
                        break;
                    }
                }
            }

            // Mostrar un mensaje dependiendo de si el usuario y la contraseña son correctos o no
            if (usuarioEncontrado)
            {
                MessageBox.Show("Inicio de sesión exitoso.");
            }
            else
            {
                MessageBox.Show("El usuario o la contraseña son incorrectos.");
            }

            // Limpiar los campos de texto
            txtUsuario.Clear();
            txtContraseña.Clear();

            Form ventana2 = new Lavado();
            ventana2.Show();
            this.Hide();
        }

        private void btnGestionar_Click(object sender, EventArgs e)
        {
            Form ventana1 = new Eliminar();
            ventana1.Show();
        }
    }
}
