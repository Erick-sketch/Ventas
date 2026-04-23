using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace Ventas
{
    public partial class FormVentas : Form
    {
        private readonly string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VentasVentas.txt");

        public FormVentas()
        {
            InitializeComponent();
            CargarVentas();
        }

        private void CargarVentas()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("El archivo de ventas no existe en:\n" + rutaArchivo, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Limpiar ListBoxes
                var listboxes = Controls.OfType<ListBox>().ToList();
                foreach (var lb in listboxes)
                    lb.Items.Clear();

                int registrosVentas = 0;
                decimal totalVentas = 0;

                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = arch.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        string[] datos = linea.Split(',');
                        // Formato: IdVenta,IdArticulo,Descripcion,Cantidad,PrecioUnitario,Total,Fecha
                        if (datos.Length >= 7 && listboxes.Count >= 4)
                        {
                            listboxes[0].Items.Add(datos[0]); // ID Venta
                            listboxes[1].Items.Add(datos[2]); // Descripción
                            listboxes[2].Items.Add(datos[3]); // Cantidad
                            listboxes[3].Items.Add(datos[5]); // Total

                            registrosVentas++;
                            if (decimal.TryParse(datos[5], out decimal total))
                                totalVentas += total;
                        }
                    }
                }

                MessageBox.Show($"Se cargaron {registrosVentas} ventas.\nTotal: ${totalVentas:F2}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txbdescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
