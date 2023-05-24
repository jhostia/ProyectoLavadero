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
    public partial class VentanaGeneral : Form
    {
        public VentanaGeneral()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void AbrirFormHija(object formhija)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();

        }
        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new VentanaInicio());
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (VentanaInicio.validarInicio == 1)
            {
                AbrirFormHija(new VentanaInfo());
            }
            else
            {
                MessageBox.Show($"Por favor inicie sesion");
            }
        }

        private void btnConsultaree_Click(object sender, EventArgs e)
        {
            if (VentanaInicio.validarInicio == 1)
            {
                AbrirFormHija(new VentanaConsultar());
            }
            else
            {
                MessageBox.Show($"Por favor inicie sesion");
            }
        }
    }
}