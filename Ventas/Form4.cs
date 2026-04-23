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

            // Conectar eventos de botones
            Controls.OfType<Button>().FirstOrDefault(b => b.Name == "btnAgregar").Click += BtnAgregar_Click;
            Controls.OfType<Button>().FirstOrDefault(b => b.Name == "btnEliminar").Click += BtnEliminar_Click;
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

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string clave = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txbclave")?.Text.Trim() ?? string.Empty;
            string descripcion = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txbdescripcion")?.Text.Trim() ?? string.Empty;
            string unidades = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txbunidades")?.Text.Trim() ?? string.Empty;
            string precio = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txbprecio")?.Text.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(unidades) || string.IsNullOrEmpty(precio))
            {
                MessageBox.Show("Todos los campos son requeridos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(unidades, out decimal cantidadNum) || !decimal.TryParse(precio, out decimal precioNum))
            {
                MessageBox.Show("Unidades y Precio deben ser números válidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Generar ID de venta (timestamp)
                string idVenta = DateTime.Now.Ticks.ToString();
                decimal total = cantidadNum * precioNum;
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Formato: IdVenta,IdArticulo,Descripcion,Cantidad,PrecioUnitario,Total,Fecha
                string registro = $"{idVenta},{clave},{descripcion},{unidades},{precio},{total:F2},{fecha}";

                using (StreamWriter arch = new StreamWriter(rutaArchivo, true))
                {
                    arch.WriteLine(registro);
                }

                MessageBox.Show($"Venta registrada correctamente.\nTotal: ${total:F2}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposVenta();
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            var listboxes = Controls.OfType<ListBox>().ToList();
            if (listboxes.Count < 1 || listboxes[0].SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona una venta para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idVentaSeleccionada = listboxes[0].SelectedItem?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(idVentaSeleccionada))
                return;

            try
            {
                if (!File.Exists(rutaArchivo))
                    return;

                List<string> registros = new List<string>();
                bool encontrado = false;

                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = arch.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        string[] datos = linea.Split(',');
                        if (datos.Length >= 1 && datos[0] != idVentaSeleccionada)
                        {
                            registros.Add(linea);
                        }
                        else if (datos.Length >= 1 && datos[0] == idVentaSeleccionada)
                        {
                            encontrado = true;
                        }
                    }
                }

                if (encontrado)
                {
                    using (StreamWriter arch = new StreamWriter(rutaArchivo, false))
                    {
                        foreach (string reg in registros)
                            arch.WriteLine(reg);
                    }

                    MessageBox.Show("Venta eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarVentas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposVenta()
        {
            var textboxes = Controls.OfType<TextBox>().ToList();
            foreach (var tb in textboxes)
                tb.Clear();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
