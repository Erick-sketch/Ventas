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
            btnPersona.Click += BtnPersona_Click;
            btnArt.Click += BtnArt_Click;
            btnVentas.Click += BtnVentas_Click;
        }

        private void BtnPersona_Click(object sender, EventArgs e)
        {
            Personal frm = new Personal();
            frm.Show();
        }

        private void BtnArt_Click(object sender, EventArgs e)
        {
            CatalogoArt frm = new CatalogoArt();
            frm.Show();
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            FormVentas frm = new FormVentas();
            frm.Show();
        }
    }
}
