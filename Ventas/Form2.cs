using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ventas
{
    public partial class Administrativo : Form
    {
        public Administrativo()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Personal frm = new Personal();
            frm.ShowDialog();
        }

        private void pictBxArticulo_Click(object sender, EventArgs e)
        {
            CatalogoArt frm = new CatalogoArt();
            frm.ShowDialog();
        }

        private void pictBxVentas_Click(object sender, EventArgs e)
        {
            FormVentas frm = new FormVentas();
            frm.ShowDialog();
        }
    }
}