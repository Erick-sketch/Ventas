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
        private readonly string rutaArchivo = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "VentasArticulos.txt");

        public CatalogoArt()
        {
            InitializeComponent();
            CargarArticulos();

            // Eventos (usa tus botones del diseño)
            btnAgregar.Click += BtnAgregar_Click;
            btnConsultar.Click += BtnConsultar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnEliminar.Click += BtnEliminar_Click;
        }

        
        private void CargarArticulos()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                    return;

                

                foreach (var linea in File.ReadAllLines(rutaArchivo))
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split(',');

                    if (datos.Length >= 5)
                    {
                        textBox1.Text = datos[0]; 
                        textBox2.Text = datos[1];
                        textBox4.Text = datos[3]; 
                        textBox5.Text = datos[4]; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }



        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string clave = textBox1.Text.Trim();
            string descripcion = textBox2.Text.Trim();
            string evidencias = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Clave y descripción son obligatorias.");
                return;
            }

            if (!decimal.TryParse(textBox4.Text, out decimal costo))
            {
                MessageBox.Show("Costo inválido.");
                return;
            }

            if (!decimal.TryParse(textBox5.Text, out decimal precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            if (ExisteClave(clave))
            {
                MessageBox.Show("La clave ya existe.");
                return;
            }

            try
            {
                string registro = $"{clave},{descripcion},{evidencias},{costo:F2},{precio:F2}";
                File.AppendAllText(rutaArchivo, registro + Environment.NewLine);

                MessageBox.Show("Artículo agregado.");
                LimpiarCampos();
                CargarArticulos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función editar aún no implementada");
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función eliminar aún no implementada");
        }

        private bool ExisteClave(string clave)
        {
            if (!File.Exists(rutaArchivo))
                return false;

            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                var datos = linea.Split(',');
                if (datos.Length > 0 && datos[0].Trim() == clave)
                    return true;
            }

            return false;
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}
