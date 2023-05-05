namespace Grafica
{
    partial class Registrar
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
            this.txtReUsuario = new System.Windows.Forms.TextBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.txtCon = new System.Windows.Forms.TextBox();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.btnRUsuario = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtReUsuario
            // 
            this.txtReUsuario.Location = new System.Drawing.Point(153, 78);
            this.txtReUsuario.Name = "txtReUsuario";
            this.txtReUsuario.Size = new System.Drawing.Size(132, 20);
            this.txtReUsuario.TabIndex = 0;
            this.txtReUsuario.TextChanged += new System.EventHandler(this.txtReUsuario_TextChanged);
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(153, 116);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(132, 20);
            this.txtCorreo.TabIndex = 1;
            // 
            // txtCon
            // 
            this.txtCon.Location = new System.Drawing.Point(153, 155);
            this.txtCon.Name = "txtCon";
            this.txtCon.Size = new System.Drawing.Size(132, 20);
            this.txtCon.TabIndex = 2;
            // 
            // txtConfirmar
            // 
            this.txtConfirmar.Location = new System.Drawing.Point(153, 198);
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.Size = new System.Drawing.Size(132, 20);
            this.txtConfirmar.TabIndex = 3;
            // 
            // btnRUsuario
            // 
            this.btnRUsuario.Location = new System.Drawing.Point(182, 251);
            this.btnRUsuario.Name = "btnRUsuario";
            this.btnRUsuario.Size = new System.Drawing.Size(75, 23);
            this.btnRUsuario.TabIndex = 4;
            this.btnRUsuario.Text = "Registrar";
            this.btnRUsuario.UseVisualStyleBackColor = true;
            this.btnRUsuario.Click += new System.EventHandler(this.btnRUsuario_Click);
            // 
            // Registrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 473);
            this.Controls.Add(this.btnRUsuario);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.txtCon);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.txtReUsuario);
            this.Name = "Registrar";
            this.Text = "Registrar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReUsuario;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.TextBox txtCon;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Button btnRUsuario;
    }
}