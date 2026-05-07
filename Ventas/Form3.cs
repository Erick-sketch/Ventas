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
                        textBox1.Text = datos[0].Trim(); 
                        textBox2.Text = datos[1].Trim();
                        textBox3.Text = datos[2].Trim();
                        textBox4.Text = datos[3].Trim(); 
                        textBox5.Text = datos[4].Trim(); 
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

            if (!int.TryParse(textBox3.Text, out int existencias))
            {
                MessageBox.Show("Existencias inválidas.");
                return;
            }

            if (existencias < 0)
            {
                MessageBox.Show("Las existencias no pueden ser negativas.");
                return;
            }

            if (!decimal.TryParse(textBox4.Text, out decimal costo))
            {
                MessageBox.Show("Costo inválido.");
                return;
            }

            if (costo <= 0)
            {
                MessageBox.Show("El costo debe ser mayor que cero.");
                return;
            }

            if (!decimal.TryParse(textBox5.Text, out decimal precio))
            {
                MessageBox.Show("Precio inválido.");
                return;
            }

            if (precio < costo)
            {
                MessageBox.Show("El precio no puede ser menor al costo.");
                return;
            }

            if (precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que cero.");
                return;
            }

            if (ExisteClave(clave))
            {
                MessageBox.Show("La clave ya existe.");
                return;
            }

            try
            {
                string registro = $"{clave},{descripcion},{existencias},{costo:F2},{precio:F2}";
                File.AppendAllText(rutaArchivo, registro + Environment.NewLine);

                MessageBox.Show("Artículo agregado.");
                LimpiarCampos();
                
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
            string claveBuscar = textBox1.Text.Trim();

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

                foreach (var linea in File.ReadAllLines(rutaArchivo))
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split(',');
                    if (datos.Length >= 5 && datos[0].Trim() == claveBuscar)
                    {
                        textBox2.Text = datos[1].Trim();
                        textBox3.Text = datos[2].Trim();
                        textBox4.Text = datos[3].Trim();
                        textBox5.Text = datos[4].Trim();

                        MessageBox.Show("Artículo encontrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
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
            string clave = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Ingresa la clave a editar.");
                return;
            }

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int existencias))
            {
                MessageBox.Show("Existencias inválidas.");
                return;
            }

            if (existencias < 0)
            {
                MessageBox.Show("Las existencias no pueden ser negativas.");
                return;
            }

            List<string> registros = new List<string>();
            bool encontrado = false;

            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                var datos = linea.Split(',');

                if (datos.Length > 0 && datos[0].Trim() == clave)
                {
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

                    if (precio < costo)
                    {
                        MessageBox.Show("El precio no puede ser menor al costo.");
                        return;
                    }

                    string nuevaLinea =
                        $"{textBox1.Text.Trim()}," +
                        $"{textBox2.Text.Trim()}," +
                        $"{existencias}," +
                        $"{costo:F2}," +
                        $"{precio:F2}";
                    registros.Add(nuevaLinea);
                    encontrado = true;
                }
                else
                {
                    registros.Add(linea);
                }
            }

            if (encontrado)
            {
                File.WriteAllLines(rutaArchivo, registros);
                MessageBox.Show("Artículo actualizado correctamente.");
                LimpiarCampos();
                
            }
            else
            {
                MessageBox.Show("No se encontró el artículo.");
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string clave = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Ingresa la clave a eliminar.");
                return;
            }

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo.");
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este artículo?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            List<string> registros = new List<string>();
            bool eliminado = false;

            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                var datos = linea.Split(',');

                if (datos.Length > 0 && datos[0].Trim() == clave)
                {
                    eliminado = true;
                }
                else
                {
                    registros.Add(linea);
                }
            }

            if (eliminado)
            {
                File.WriteAllLines(rutaArchivo, registros);
                MessageBox.Show("Artículo eliminado correctamente.");
                LimpiarCampos();
                
            }
            else
            {
                MessageBox.Show("No se encontró el artículo.");
            }
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
            textBox1.Focus();
        }
    }
}
