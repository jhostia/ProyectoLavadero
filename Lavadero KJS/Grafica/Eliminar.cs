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
    public partial class Eliminar : Form
    {
        public Eliminar()
        {
            InitializeComponent();
        }

        private void txtUsuarioAEliminar_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nombreUsuarioAEliminar = txtUsuarioAEliminar.Text.Trim();
            string rutaArchivo = "usuarios.txt";

            // Verificar que el usuario exista en el archivo
            if (!File.ReadLines(rutaArchivo).Any(line => line.StartsWith(nombreUsuarioAEliminar)))
            {
                MessageBox.Show($"El usuario {nombreUsuarioAEliminar} no existe.");
                return;
            }

            // Crear una nueva lista de usuarios sin el usuario a eliminar
            List<string> listaUsuarios = File.ReadAllLines(rutaArchivo).Where(line => !line.StartsWith(nombreUsuarioAEliminar)).ToList();

            // Sobreescribir el archivo con la nueva lista de usuarios
            File.WriteAllLines(rutaArchivo, listaUsuarios);

            MessageBox.Show($"El usuario {nombreUsuarioAEliminar} ha sido eliminado.");
            txtUsuarioAEliminar.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Leer el texto ingresado en el cuadro de búsqueda
            string busqueda = txtUsuarioAEliminar.Text.Trim();

            // Abrir el archivo de usuarios y buscar la información del usuario
            string rutaArchivo = "usuarios.txt";
            bool encontrado = false;
            string usuario = "";
            string correo = "";

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datosUsuario = linea.Split(',');

                    if (datosUsuario[0] == busqueda)
                    {
                        encontrado = true;
                        usuario = datosUsuario[0];
                        correo = datosUsuario[1];
                        break;
                    }
                }
            }

            // Mostrar los resultados de la búsqueda
            if (encontrado)
            {
                MessageBox.Show($"Usuario encontrado: {usuario} ({correo})");
            }
            else
            {
                MessageBox.Show("Usuario no encontrado");
            }
        }
    }
}
