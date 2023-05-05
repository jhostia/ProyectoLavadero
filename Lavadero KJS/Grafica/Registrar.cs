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
    public partial class Registrar : Form
    {
        public Registrar()
        {
            InitializeComponent();
        }

        private void btnRUsuario_Click(object sender, EventArgs e)
        {
            Registrar registrar = new Registrar();  
            // Leer los datos ingresados en los campos de texto
            string nombreUsuario = txtReUsuario.Text.Trim();
            string correoElectronico = txtCorreo.Text.Trim();
            string contrasena = txtCon.Text.Trim();
            string confirmarContrasena = txtConfirmar.Text.Trim();

            // Validar que los campos no estén vacíos y que la contraseña y su confirmación sean iguales
            if (nombreUsuario == "" || correoElectronico == "" || contrasena == "" || confirmarContrasena == "")
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            if (contrasena != confirmarContrasena)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor verifique.");
                return;
            }

            // Abrir el archivo de texto donde se guardarán los datos de los usuarios
            string rutaArchivo = "usuarios.txt";

            using (StreamWriter sw = File.AppendText(rutaArchivo))
            {
                // Escribir los datos del usuario en el archivo de texto
                sw.WriteLine($"{nombreUsuario},{correoElectronico},{contrasena}");
            }

            // Mostrar un mensaje de confirmación
            MessageBox.Show("Usuario registrado con éxito.");
            registrar.Close();

            // Limpiar los campos de texto
            txtReUsuario.Clear();
            txtCorreo.Clear();
            txtCon.Clear();
            txtConfirmar.Clear();

            this.Close();
        }

        private void txtReUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
