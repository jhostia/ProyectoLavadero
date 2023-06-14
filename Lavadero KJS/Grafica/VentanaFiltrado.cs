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
    public partial class VentanaFiltrado : Form
    {
        LogicaLavadero logicaLavadero;
        DataTable dt = new DataTable();
        public VentanaFiltrado()
        {
            logicaLavadero = new LogicaLavadero();
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            foreach (var servicio in logicaLavadero.ObtenerServicios())
            {
                tablaInfo.Rows.Add(servicio.Cliente.Nombre, servicio.Cliente.Documento,
                    servicio.Cliente.Telefono, servicio.Vehiculo.Marca, servicio.Vehiculo.Modelo,
                    servicio.Vehiculo.Placa, servicio.FechaServicio.ToString("dd/MM/yyyy"), 
                    servicio.FechaServicio.ToString("HH:mm:ss"), servicio.ValorServicio, servicio.ValorAdicional, 
                    servicio.ValorTotal);
            }
        }
       
        private void txtConsultar_TextChanged(object sender, EventArgs e)
        {
            if (txtConsultar.Text != string.Empty)
            {
                tablaInfo.CurrentCell = null;
                foreach(DataGridViewRow fila in tablaInfo.Rows)
                {
                    fila.Visible = false;
                }
                foreach(DataGridViewRow fila in tablaInfo.Rows)
                {
                    foreach(DataGridViewCell celda in fila.Cells)
                    {
                        if((celda.Value.ToString().ToUpper()).IndexOf(txtConsultar.Text.ToUpper()) == 0)
                        {
                            fila.Visible = true;
                            break;
                        }
                    }
                }
            }else
            {
                tablaInfo.Rows.Clear();
                CargarDatos();
            }
        }
    }
}
