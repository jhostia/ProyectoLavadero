﻿using Entidades;
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


namespace Presentacion
{
   
    public partial class VentanaInicio : Form
    {

        private DatosServicios datosServicios; // Declarar la instancia de DatosServicios
        public static int validarInicio = 0;
        public VentanaInicio()
        {
            InitializeComponent();

            // Inicializar la instancia de DatosServicios
            datosServicios = new DatosServicios();
        }
        private void btnRegiInicio_Click(object sender, EventArgs e)
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
                pnlBien.Visible = true;
                VentanaInicio.validarInicio = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}");
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
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

            pnlIniciar.Visible = true;
            pnlRegistrar.Visible = false;
        }

    }
}