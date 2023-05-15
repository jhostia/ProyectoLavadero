using Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





namespace Datos
{
    public class DatosUsuarios
    {
        private readonly string _rutaArchivo = "usuarios.txt";

        public void GuardarUsuario(Usuario usuario)
        {
            using (StreamWriter sw = File.AppendText(_rutaArchivo))
            {
                // Escribir los datos del usuario en el archivo de texto
                sw.WriteLine($"{usuario.NombreUsuario},{usuario.CorreoElectronico},{usuario.Contrasena}");
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            if (!File.Exists(_rutaArchivo))
            {
                // Si el archivo no existe, crearlo vacío
                File.Create(_rutaArchivo).Close();
            }

            List<Usuario> usuarios = new List<Usuario>();

            using (StreamReader sr = new StreamReader(_rutaArchivo))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datosUsuario = linea.Split(',');
                    Usuario usuario = new Usuario
                    {
                        NombreUsuario = datosUsuario[0],
                        CorreoElectronico = datosUsuario[1],
                        Contrasena = datosUsuario[2]
                    };
                    usuarios.Add(usuario);
                }
            }

            return usuarios;
        }
    }
}



