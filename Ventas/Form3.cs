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
    public partial class CatalogoArt : Form
    {
        private readonly string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VentasArticulos.txt");

        public CatalogoArt()
        {
            InitializeComponent();
            CargarArticulos();

            // Conectar evento del botón Consultar
            Controls.OfType<Button>().FirstOrDefault(b => b.Name == "button2").Click += BtnConsultar_Click;
        }

        private void CargarArticulos()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("El archivo de artículos no existe en:\n" + rutaArchivo, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Limpiar ListBoxes
                if (Controls.OfType<ListBox>().Count() >= 4)
                {
                    var listboxes = Controls.OfType<ListBox>().ToList();
                    foreach (var lb in listboxes)
                        lb.Items.Clear();

                    using (StreamReader arch = new StreamReader(rutaArchivo))
                    {
                        string linea;
                        while ((linea = arch.ReadLine()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(linea))
                                continue;

                            string[] datos = linea.Split(',');
                            if (datos.Length >= 4)
                            {
                                listboxes[0].Items.Add(datos[0]); // Clave
                                listboxes[1].Items.Add(datos[1]); // Descripción
                                listboxes[2].Items.Add(datos[3]); // Costo
                                listboxes[3].Items.Add(datos[4]); // Precio
                            }
                        }
                    }

                    MessageBox.Show($"Se cargaron {listboxes[0].Items.Count} artículos correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar artículos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            string claveBuscar = Controls.OfType<TextBox>().FirstOrDefault(t => t.Name == "textBox1")?.Text.Trim();

            if (string.IsNullOrEmpty(claveBuscar))
            {
                MessageBox.Show("Ingresa una clave para buscar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("El archivo de artículos no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = arch.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        string[] datos = linea.Split(',');
                        if (datos.Length >= 5 && datos[0].Trim() == claveBuscar)
                        {
                            string mensaje = $"Datos del Artículo\n\n" +
                                           $"Clave: {datos[0].Trim()}\n" +
                                           $"Nombre: {datos[1].Trim()}\n" +
                                           $"Descripción: {datos[2].Trim()}\n" +
                                           $"Costo: ${datos[3].Trim()}\n" +
                                           $"Precio: ${datos[4].Trim()}";

                            MessageBox.Show(mensaje, "Información del Artículo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                MessageBox.Show($"No se encontró artículo con clave: {claveBuscar}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar artículo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
