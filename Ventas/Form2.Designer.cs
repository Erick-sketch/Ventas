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
            btnPersona = new Button();
            btnArt = new Button();
            btnVentas = new Button();
            SuspendLayout();
            // 
            // btnPersona
            // 
            btnPersona.Location = new Point(27, 72);
            btnPersona.Name = "btnPersona";
            btnPersona.Size = new Size(250, 313);
            btnPersona.TabIndex = 0;
            btnPersona.Text = "Personal";
            btnPersona.UseVisualStyleBackColor = true;
            btnPersona.Click += btnPersona_Click_1;
            // 
            // btnArt
            // 
            btnArt.Location = new Point(328, 72);
            btnArt.Name = "btnArt";
            btnArt.Size = new Size(250, 313);
            btnArt.TabIndex = 1;
            btnArt.Text = "Articulos";
            btnArt.UseVisualStyleBackColor = true;
            // 
            // btnVentas
            // 
            btnVentas.Location = new Point(623, 72);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(250, 313);
            btnVentas.TabIndex = 2;
            btnVentas.Text = "Ventas";
            btnVentas.UseVisualStyleBackColor = true;
            // 
            // Administrativo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 450);
            Controls.Add(btnVentas);
            Controls.Add(btnArt);
            Controls.Add(btnPersona);
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