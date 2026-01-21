namespace DonJulioSuper.Forms
{
    partial class FrmCaja
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
            this.btnApertura = new System.Windows.Forms.Button();
            this.btnCierre = new System.Windows.Forms.Button();
            this.txtMontoApertura = new System.Windows.Forms.TextBox();
            this.txtCajaID = new System.Windows.Forms.TextBox();
            this.txtMontoCierre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnApertura
            // 
            this.btnApertura.Location = new System.Drawing.Point(102, 323);
            this.btnApertura.Name = "btnApertura";
            this.btnApertura.Size = new System.Drawing.Size(75, 23);
            this.btnApertura.TabIndex = 0;
            this.btnApertura.Text = "button1";
            this.btnApertura.UseVisualStyleBackColor = true;
            this.btnApertura.Click += new System.EventHandler(this.btnApertura_Click);
            // 
            // btnCierre
            // 
            this.btnCierre.Location = new System.Drawing.Point(317, 323);
            this.btnCierre.Name = "btnCierre";
            this.btnCierre.Size = new System.Drawing.Size(75, 23);
            this.btnCierre.TabIndex = 1;
            this.btnCierre.Text = "button1";
            this.btnCierre.UseVisualStyleBackColor = true;
            this.btnCierre.Click += new System.EventHandler(this.btnCierre_Click);
            // 
            // txtMontoApertura
            // 
            this.txtMontoApertura.Location = new System.Drawing.Point(258, 90);
            this.txtMontoApertura.Name = "txtMontoApertura";
            this.txtMontoApertura.Size = new System.Drawing.Size(100, 22);
            this.txtMontoApertura.TabIndex = 2;
            // 
            // txtCajaID
            // 
            this.txtCajaID.Location = new System.Drawing.Point(258, 137);
            this.txtCajaID.Name = "txtCajaID";
            this.txtCajaID.Size = new System.Drawing.Size(100, 22);
            this.txtCajaID.TabIndex = 3;
            // 
            // txtMontoCierre
            // 
            this.txtMontoCierre.Location = new System.Drawing.Point(258, 191);
            this.txtMontoCierre.Name = "txtMontoCierre";
            this.txtMontoCierre.Size = new System.Drawing.Size(100, 22);
            this.txtMontoCierre.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Monto de apertura ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Monto de Cierre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "ID de Caja";
            // 
            // FrmCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMontoCierre);
            this.Controls.Add(this.txtCajaID);
            this.Controls.Add(this.txtMontoApertura);
            this.Controls.Add(this.btnCierre);
            this.Controls.Add(this.btnApertura);
            this.Name = "FrmCaja";
            this.Text = "FrmCaja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApertura;
        private System.Windows.Forms.Button btnCierre;
        private System.Windows.Forms.TextBox txtMontoApertura;
        private System.Windows.Forms.TextBox txtCajaID;
        private System.Windows.Forms.TextBox txtMontoCierre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}