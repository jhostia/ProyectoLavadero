namespace Grafica
{
    partial class Lavado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLvado = new System.Windows.Forms.Button();
            this.btnEnjuague = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlVehiculo = new System.Windows.Forms.Panel();
            this.btnCarro = new System.Windows.Forms.Button();
            this.btnMoto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlOpcion = new System.Windows.Forms.Panel();
            this.pnlVehiculo.SuspendLayout();
            this.pnlOpcion.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLvado
            // 
            this.btnLvado.Location = new System.Drawing.Point(30, 92);
            this.btnLvado.Name = "btnLvado";
            this.btnLvado.Size = new System.Drawing.Size(249, 45);
            this.btnLvado.TabIndex = 0;
            this.btnLvado.Text = "Lavado Completo";
            this.btnLvado.UseVisualStyleBackColor = true;
            this.btnLvado.Click += new System.EventHandler(this.btnLvado_Click);
            // 
            // btnEnjuague
            // 
            this.btnEnjuague.Location = new System.Drawing.Point(30, 172);
            this.btnEnjuague.Name = "btnEnjuague";
            this.btnEnjuague.Size = new System.Drawing.Size(249, 45);
            this.btnEnjuague.TabIndex = 1;
            this.btnEnjuague.Text = "Enjuague";
            this.btnEnjuague.UseVisualStyleBackColor = true;
            this.btnEnjuague.Click += new System.EventHandler(this.btnEnjuague_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seleccione el tipo de servicio : ";
            // 
            // pnlVehiculo
            // 
            this.pnlVehiculo.Controls.Add(this.label2);
            this.pnlVehiculo.Controls.Add(this.btnMoto);
            this.pnlVehiculo.Controls.Add(this.btnCarro);
            this.pnlVehiculo.Location = new System.Drawing.Point(9, 9);
            this.pnlVehiculo.Name = "pnlVehiculo";
            this.pnlVehiculo.Size = new System.Drawing.Size(339, 270);
            this.pnlVehiculo.TabIndex = 4;
            this.pnlVehiculo.Visible = false;
            // 
            // btnCarro
            // 
            this.btnCarro.Location = new System.Drawing.Point(45, 92);
            this.btnCarro.Name = "btnCarro";
            this.btnCarro.Size = new System.Drawing.Size(249, 45);
            this.btnCarro.TabIndex = 1;
            this.btnCarro.Text = "Carro";
            this.btnCarro.UseVisualStyleBackColor = true;
            // 
            // btnMoto
            // 
            this.btnMoto.Location = new System.Drawing.Point(45, 172);
            this.btnMoto.Name = "btnMoto";
            this.btnMoto.Size = new System.Drawing.Size(249, 45);
            this.btnMoto.TabIndex = 2;
            this.btnMoto.Text = "Moto";
            this.btnMoto.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Seleccione el tipo de vehiculo: ";
            // 
            // pnlOpcion
            // 
            this.pnlOpcion.Controls.Add(this.label1);
            this.pnlOpcion.Controls.Add(this.btnEnjuague);
            this.pnlOpcion.Controls.Add(this.btnLvado);
            this.pnlOpcion.Location = new System.Drawing.Point(9, 12);
            this.pnlOpcion.Name = "pnlOpcion";
            this.pnlOpcion.Size = new System.Drawing.Size(339, 270);
            this.pnlOpcion.TabIndex = 3;
            // 
            // Lavado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 294);
            this.Controls.Add(this.pnlVehiculo);
            this.Controls.Add(this.pnlOpcion);
            this.Name = "Lavado";
            this.Text = "Lavado";
            this.pnlVehiculo.ResumeLayout(false);
            this.pnlVehiculo.PerformLayout();
            this.pnlOpcion.ResumeLayout(false);
            this.pnlOpcion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLvado;
        private System.Windows.Forms.Button btnEnjuague;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlVehiculo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMoto;
        private System.Windows.Forms.Button btnCarro;
        private System.Windows.Forms.Panel pnlOpcion;
    }
}