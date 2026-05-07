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
            pictbxPersonal = new PictureBox();
            pictBxArticulo = new PictureBox();
            pictBxVentas = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictbxPersonal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictBxArticulo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictBxVentas).BeginInit();
            SuspendLayout();
            // 
            // pictbxPersonal
            // 
            pictbxPersonal.Image = (Image)resources.GetObject("pictbxPersonal.Image");
            pictbxPersonal.Location = new Point(12, 81);
            pictbxPersonal.Name = "pictbxPersonal";
            pictbxPersonal.Size = new Size(310, 295);
            pictbxPersonal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictbxPersonal.TabIndex = 3;
            pictbxPersonal.TabStop = false;
            pictbxPersonal.Click += pictureBox1_Click;
            // 
            // pictBxArticulo
            // 
            pictBxArticulo.Image = (Image)resources.GetObject("pictBxArticulo.Image");
            pictBxArticulo.Location = new Point(328, 81);
            pictBxArticulo.Name = "pictBxArticulo";
            pictBxArticulo.Size = new Size(310, 295);
            pictBxArticulo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictBxArticulo.TabIndex = 4;
            pictBxArticulo.TabStop = false;
            pictBxArticulo.Click += pictBxArticulo_Click;
            // 
            // pictBxVentas
            // 
            pictBxVentas.Image = (Image)resources.GetObject("pictBxVentas.Image");
            pictBxVentas.Location = new Point(650, 81);
            pictBxVentas.Name = "pictBxVentas";
            pictBxVentas.Size = new Size(310, 295);
            pictBxVentas.SizeMode = PictureBoxSizeMode.StretchImage;
            pictBxVentas.TabIndex = 5;
            pictBxVentas.TabStop = false;
            pictBxVentas.Click += pictBxVentas_Click;
            // 
            // Administrativo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 450);
            Controls.Add(pictBxVentas);
            Controls.Add(pictBxArticulo);
            Controls.Add(pictbxPersonal);
            Name = "Administrativo";
            Text = "Administrativo";
            ((System.ComponentModel.ISupportInitialize)pictbxPersonal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictBxArticulo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictBxVentas).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictbxPersonal;
        private PictureBox pictBxArticulo;
        private PictureBox pictBxVentas;
    }
}