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
    public partial class Lavado : Form
    {
        public Lavado()
        {
            InitializeComponent();
        }

        private void btnLvado_Click(object sender, EventArgs e)
        {
          
            // Mostrar el panel correspondiente a la opción de lavado completo
            pnlVehiculo.Visible = true;
        }

        private void btnEnjuague_Click(object sender, EventArgs e)
        {
            

            // Mostrar el panel correspondiente a la opción de enjuague
            pnlVehiculo.Visible = true;
        }
    }
}
