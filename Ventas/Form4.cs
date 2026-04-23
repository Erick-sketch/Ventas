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
            Controls.OfType<Button>().FirstOrDefault(b => b.Name == "btnRegistar").Click += BtnRegistrar_Click;
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

                // Agregar a la lista de detalle
                AgregarAlDetalle(clave, descripcion, unidades, precio, total);

                LimpiarCamposVenta();
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarAlDetalle(string clave, string descripcion, string unidades, string precio, decimal total)
        {
            var listDetalleVenta = Controls.OfType<ListBox>().FirstOrDefault(l => l.Name == "list_detalleventa");
            if (listDetalleVenta != null)
            {
                string linea = $"Clave: {clave} | Desc: {descripcion} | Cantidad: {unidades} | Precio: ${precio} | Total: ${total:F2}";
                listDetalleVenta.Items.Add(linea);
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

        private void button1_Click(object sender, EventArgs e)
        {
            BtnRegistrar_Click(sender, e);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            var listboxes = Controls.OfType<ListBox>().ToList();

            // Validar que haya artículos en la venta
            if (listboxes.Count < 1 || listboxes[0].Items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un artículo a la venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Leer todas las ventas del listbox principal
                decimal totalVenta = 0;
                bool ventasValidas = true;

                for (int i = 0; i < listboxes[0].Items.Count; i++)
                {
                    string idVenta = listboxes[0].Items[i]?.ToString() ?? string.Empty;
                    string descripcion = listboxes[1].Items.Count > i ? listboxes[1].Items[i]?.ToString() ?? string.Empty : string.Empty;
                    string cantidadStr = listboxes[2].Items.Count > i ? listboxes[2].Items[i]?.ToString() ?? string.Empty : string.Empty;
                    string precioStr = listboxes[3].Items.Count > i ? listboxes[3].Items[i]?.ToString() ?? string.Empty : string.Empty;

                    if (decimal.TryParse(cantidadStr, out decimal cantidad) && decimal.TryParse(precioStr, out decimal precio))
                    {
                        totalVenta += cantidad * precio;
                    }
                    else
                    {
                        ventasValidas = false;
                        break;
                    }
                }

                if (!ventasValidas)
                {
                    MessageBox.Show("Algunos datos de la venta no son válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Actualizar el total en el textBox5
                var textbox5 = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "textBox5");
                if (textbox5 != null)
                    textbox5.Text = totalVenta.ToString("F2");

                // Agregar el importe total al list_import
                if (listboxes.Count > 4)
                {
                    listboxes[4].Items.Add(totalVenta.ToString("F2"));
                }

                // También agregar al txb_importe
                var txbImporte = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "txb_importe");
                if (txbImporte != null)
                    txbImporte.Text = totalVenta.ToString("F2");

                MessageBox.Show($"Venta registrada correctamente.\nTotal: ${totalVenta:F2}\nArtículos: {listboxes[0].Items.Count}", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos y detalle
                LimpiarCamposVenta();
                LimpiarDetalle();
                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarDetalle()
        {
            var listDetalleVenta = Controls.OfType<ListBox>().FirstOrDefault(l => l.Name == "list_detalleventa");
            if (listDetalleVenta != null)
                listDetalleVenta.Items.Clear();
        }
    }
}
