namespace Ventas
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    public partial class Personal : Form
    {
        private readonly string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VentasPersonal.txt");
        private string linea = "";

        public Personal()
        {
            InitializeComponent();

            button1.Click += btnAdd_Click;
            button2.Click += btnSearch_Click;
            button3.Click += btnUpdate_Click;
            button4.Click += btnDelete_Click;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            linea = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + comboBox1.Text + "," + textBox4.Text;

            try
            {
                using (StreamWriter arch = new StreamWriter(rutaArchivo, true))
                {
                    arch.WriteLine(linea);
                }

                MessageBox.Show("El registro se ha agregado correctamente");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el registro: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string claveBuscar = textBox1.Text.Trim();

            if (claveBuscar == "")
            {
                MessageBox.Show("Ingresa una clave para buscar.");
                return;
            }


            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo de datos.");
                return;
            }

            bool encontrado = false;

            try
            {
                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    while ((linea = arch.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');

                        if (datos.Length > 0 && datos[0] == claveBuscar)
                        {
                            textBox2.Text = datos.Length > 1 ? datos[1] : string.Empty;
                            textBox3.Text = datos.Length > 2 ? datos[2] : string.Empty;
                            comboBox1.Text = datos.Length > 3 ? datos[3] : string.Empty;
                            textBox4.Text = datos.Length > 4 ? datos[4] : string.Empty;
                            encontrado = true;
                            break;
                        }
                    }
                }

                if (!encontrado)
                    MessageBox.Show("No se encontró ningún registro con esa clave.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string claveBuscar = textBox1.Text.Trim();

            if (claveBuscar == "")
            {
                MessageBox.Show("Ingresa la clave del registro a actualizar.");
                return;
            }

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo de datos.");
                return;
            }

            bool encontrado = false;
            List<string> registros = new List<string>();

            try
            {
                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    while ((linea = arch.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');

                        if (datos.Length > 0 && datos[0] == claveBuscar)
                        {
                            linea = textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + comboBox1.Text + "," + textBox4.Text;
                            encontrado = true;
                        }

                        registros.Add(linea);
                    }
                }

                if (encontrado)
                {
                    using (StreamWriter archW = new StreamWriter(rutaArchivo, false))
                    {
                        foreach (string reg in registros)
                            archW.WriteLine(reg);
                    }

                    MessageBox.Show("Registro actualizado correctamente.");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con esa clave.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el archivo: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string claveBuscar = textBox1.Text.Trim();

            if (claveBuscar == "")
            {
                MessageBox.Show("Ingresa la clave del registro a eliminar.");
                return;
            }

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo de datos.");
                return;
            }

            bool encontrado = false;
            List<string> registros = new List<string>();

            try
            {
                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    while ((linea = arch.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');

                        if (datos.Length > 0 && datos[0] == claveBuscar)
                        {
                            encontrado = true;
                        }
                        else
                        {
                            registros.Add(linea);
                        }
                    }
                }

                if (encontrado)
                {
                    using (StreamWriter archW = new StreamWriter(rutaArchivo, false))
                    {
                        foreach (string reg in registros)
                            archW.WriteLine(reg);
                    }

                    MessageBox.Show("Registro eliminado correctamente.");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con esa clave.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.Text = "";
            textBox4.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
