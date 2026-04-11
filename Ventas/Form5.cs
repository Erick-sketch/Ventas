namespace Ventas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;

    public partial class Form5 : Form
    {
        private readonly string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VentasPersonal.txt");
        valida_txt vtxt = new valida_txt();

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string clave = textBox1.Text.Trim();
            string contraseña = textBox2.Text.Trim();


            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Falta Clave o contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar credenciales en el archivo
            string cargo = VerificarCredenciales(clave, contraseña);

            if (!string.IsNullOrEmpty(cargo))
            {
                MessageBox.Show("Bienvenido " + clave, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();

                if (cargo.ToLower() == "administrador")
                {
                    Administrativo admin = new Administrativo();
                    admin.ShowDialog();
                }
                else if (cargo.ToLower() == "venta")
                {
                    FormVentas ventas = new FormVentas();
                    ventas.ShowDialog();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Clave o contraseña incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCampos();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string VerificarCredenciales(string clave, string contraseña)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("El archivo de datos no existe en:\n" + rutaArchivo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = arch.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        string[] datos = linea.Split(',');

                        // Formato: Clave,Nombre,Contraseña,Cargo,Salario
                        if (datos.Length >= 4)
                        {
                            string claveArchivo = datos[0].Trim();
                            string contraseñaArchivo = datos[2].Trim();
                            string cargoArchivo = datos[3].Trim();

                            // Comparación exacta (case-sensitive)
                            if (claveArchivo == clave && contraseñaArchivo == contraseña)
                            {
                                return cargoArchivo;
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }
    }
}
