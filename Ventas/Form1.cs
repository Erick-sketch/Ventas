namespace Ventas
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    public partial class Personal : Form
    {
        valida_txt vtxt = new valida_txt();
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
            bool v = vtxt.validaCampos(textBox1.Text, txtPSW.Text, textBox2.Text, comboBox1.Text, textBox4.Text);
            if (v)
            {
                try
                {
                    using (StreamWriter arch = new StreamWriter(rutaArchivo, true))
                    {
                        string registro = textBox1.Text + "," + textBox2.Text + "," + txtPSW.Text + "," + comboBox1.Text + "," + textBox4.Text;
                        arch.WriteLine(registro);
                    }
                    MessageBox.Show("Registro agregado correctamente.");
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al escribir en el archivo: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("La clave no puede estar vacía.");
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
                            txtPSW.Text = datos.Length > 2 ? datos[2] : string.Empty;
                            comboBox1.Text = datos.Length > 3 ? datos[3] : string.Empty;
                            textBox4.Text = datos.Length > 4 ? datos[4] : string.Empty;
                            encontrado = true;

                            // Mostrar MessageBox con todos los datos
                            string mensaje = $"Datos del Personal\n\n" +
                                           $"Clave: {datos[0].Trim()}\n" +
                                           $"Nombre: {(datos.Length > 1 ? datos[1].Trim() : "N/A")}\n" +
                                           $"Contraseña: {(datos.Length > 2 ? datos[2].Trim() : "N/A")}\n" +
                                           $"Cargo: {(datos.Length > 3 ? datos[3].Trim() : "N/A")}\n" +
                                           $"Salario: ${(datos.Length > 4 ? datos[4].Trim() : "0.00")}";

                            MessageBox.Show(mensaje, "Información del Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            linea = textBox1.Text + "," + textBox2.Text + "," + txtPSW.Text + "," + comboBox1.Text + "," + textBox4.Text;
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
            txtPSW.Clear();
            comboBox1.Text = "";
            textBox4.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPSW_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void BtnLimpiarDuplicados_Click(object sender, EventArgs e)
        {
            EliminarDuplicados();
        }

        private void EliminarDuplicados()
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                    return;

                List<string> registros = new List<string>();
                HashSet<string> clavesVistas = new HashSet<string>();
                int duplicadosEliminados = 0;

                // Leer archivo y filtrar duplicados
                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = arch.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea))
                            continue;

                        string[] datos = linea.Split(',');

                        if (datos.Length > 0)
                        {
                            string clave = datos[0].Trim();

                            // Si la clave no ha sido vista, la agregamos
                            if (!clavesVistas.Contains(clave))
                            {
                                registros.Add(linea);
                                clavesVistas.Add(clave);
                            }
                            else
                            {
                                // Es un duplicado, lo contamos pero no lo agregamos
                                duplicadosEliminados++;
                            }
                        }
                    }
                }

                // Si hay duplicados, reescribir el archivo
                if (duplicadosEliminados > 0)
                {
                    using (StreamWriter archW = new StreamWriter(rutaArchivo, false))
                    {
                        foreach (string reg in registros)
                            archW.WriteLine(reg);
                    }

                    MessageBox.Show($"Se eliminaron {duplicadosEliminados} registros duplicados.", "Limpieza completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar duplicados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }


    class valida_txt()
    {
        public bool validaID(string id)
        {
            if(id != "")
                return true;
            else
                return false;
        }

        public bool validaCampos(string clave,string nombre, string psw, string cargo, string salario)
        {
            if (clave != "" && nombre != "" && psw != "" && cargo != "" && salario != "")
                return true;
            else
               MessageBox.Show("Todos los campos deben ser llenados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}


