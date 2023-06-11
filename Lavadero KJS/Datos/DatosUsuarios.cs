using Entidades;
using MySql.Data.MySqlClient;
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
        Conexion conexion;

        public DatosUsuarios()
        {
            conexion = new Conexion();
        }

        public void GuardarUsuario(Usuario usuario)
        {
            MySqlCommand comando = new MySqlCommand($"INSERT INTO usuarios (nombre, correo, pssword) VALUES( '{usuario.NombreUsuario}', '{usuario.CorreoElectronico}', '{usuario.Contrasena}');", conexion.AbrirConexion());
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            MySqlCommand comando = new MySqlCommand("SELECT * FROM usuarios", conexion.AbrirConexion());
            MySqlDataReader consulta = comando.ExecuteReader();

            while(consulta.Read())
            {
                Usuario user = new Usuario();
                user.NombreUsuario = consulta.GetString(1);
                user.CorreoElectronico = consulta.GetString(2);
                user.Contrasena = consulta.GetString(3);
                user.ConfirmarContraseña = consulta.GetString(3);
                usuarios.Add(user);
            }

            return usuarios;
        }
    }
}



