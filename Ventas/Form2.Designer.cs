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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administrativo));
            btnPersona = new Button();
            btnArt = new Button();
            btnVentas = new Button();
            SuspendLayout();
            // 
            // btnPersona
            // 
            btnPersona.BackgroundImage = (Image)resources.GetObject("btnPersona.BackgroundImage");
            btnPersona.BackgroundImageLayout = ImageLayout.Stretch;
            btnPersona.Location = new Point(12, 81);
            btnPersona.Name = "btnPersona";
            btnPersona.Size = new Size(310, 295);
            btnPersona.TabIndex = 0;
            btnPersona.UseVisualStyleBackColor = true;
            btnPersona.Click += btnPersona_Click_1;
            // 
            // btnArt
            // 
            btnArt.BackgroundImage = (Image)resources.GetObject("btnArt.BackgroundImage");
            btnArt.BackgroundImageLayout = ImageLayout.Stretch;
            btnArt.Location = new Point(328, 81);
            btnArt.Name = "btnArt";
            btnArt.Size = new Size(310, 295);
            btnArt.TabIndex = 1;
            btnArt.UseVisualStyleBackColor = true;
            // 
            // btnVentas
            // 
            btnVentas.BackgroundImage = (Image)resources.GetObject("btnVentas.BackgroundImage");
            btnVentas.BackgroundImageLayout = ImageLayout.Stretch;
            btnVentas.Location = new Point(644, 81);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(310, 295);
            btnVentas.TabIndex = 2;
            btnVentas.UseVisualStyleBackColor = true;
            // 
            // Administrativo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 450);
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