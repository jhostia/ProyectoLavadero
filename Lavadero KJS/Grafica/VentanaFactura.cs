using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafica
{
    public class VentanaFactura : Form
    {
        private TextBox txtFactura;

        public VentanaFactura(string factura)
        {
            InitializeComponent();

            // Convierte el objeto factura a una cadena de texto
            string textoFactura = factura.ToString();

            txtFactura.Text = textoFactura;
        }

        private void InitializeComponent()
        {
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFactura
            // 
            this.txtFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFactura.Location = new System.Drawing.Point(0, 0);
            this.txtFactura.Multiline = true;
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFactura.Size = new System.Drawing.Size(400, 400);
            this.txtFactura.TabIndex = 0;
            // 
            // VentanaFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.txtFactura);
            this.Name = "VentanaFactura";
            this.Text = "Factura";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
