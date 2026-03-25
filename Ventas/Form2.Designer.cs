namespace Ventas
{
    partial class Administrativo
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
            this.btnPersona = new Button();
            this.btnArt = new Button();
            this.btnVentas = new Button();
            SuspendLayout();
            // 
            // btnPersona
            // 
            this.btnPersona.Location = new Point(27, 72);
            this.btnPersona.Name = "btnPersona";
            this.btnPersona.Size = new Size(250, 313);
            this.btnPersona.TabIndex = 0;
            this.btnPersona.Text = "Personal";
            this.btnPersona.UseVisualStyleBackColor = true;
            // 
            // btnArt
            // 
            this.btnArt.Location = new Point(328, 72);
            this.btnArt.Name = "btnArt";
            this.btnArt.Size = new Size(250, 313);
            this.btnArt.TabIndex = 1;
            this.btnArt.Text = "Articulos";
            this.btnArt.UseVisualStyleBackColor = true;
            // 
            // btnVentas
            // 
            this.btnVentas.Location = new Point(623, 72);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new Size(250, 313);
            this.btnVentas.TabIndex = 2;
            this.btnVentas.Text = "Ventas";
            this.btnVentas.UseVisualStyleBackColor = true;
            // 
            // Administrativo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 450);
            Controls.Add(this.btnVentas);
            Controls.Add(this.btnArt);
            Controls.Add(this.btnPersona);
            Name = "Administrativo";
            Text = "Administrativo";
            ResumeLayout(false);
        }

        #endregion

        private Button btnPersona;
        private Button btnArt;
        private Button btnVentas;
    }
}