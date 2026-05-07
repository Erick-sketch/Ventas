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
            // Validar todos los campos con reglas de seguridad
            if (vtxt.validaCampos(textBox1.Text, txtPSW.Text, textBox2.Text, comboBox1.Text, textBox4.Text))
            {
                try
                {
                    // Verificar que la clave no exista ya
                    if (VerificarClaveExistente(textBox1.Text.Trim()))
                    {
                        MessageBox.Show("La clave ya existe en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (StreamWriter arch = new StreamWriter(rutaArchivo, true))
                    {
                        string registro = textBox1.Text.Trim() + "," + textBox2.Text.Trim() + "," + txtPSW.Text.Trim() + "," + comboBox1.Text.Trim() + "," + textBox4.Text.Trim();
                        arch.WriteLine(registro);
                    }
                    MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al escribir en el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool VerificarClaveExistente(string clave)
        {
            if (!File.Exists(rutaArchivo))
                return false;

            try
            {
                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    while ((linea = arch.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');
                        if (datos.Length > 0 && datos[0].Trim() == clave)
                            return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string claveBuscar = textBox1.Text.Trim();

            // Validar la búsqueda
            if (!vtxt.ValidarBusqueda(claveBuscar))
                return;

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        if (datos.Length > 0 && datos[0].Trim() == claveBuscar)
                        {
                            textBox2.Text = datos.Length > 1 ? datos[1].Trim() : string.Empty;
                            txtPSW.Text = datos.Length > 2 ? datos[2].Trim() : string.Empty;
                            comboBox1.Text = datos.Length > 3 ? datos[3].Trim() : string.Empty;
                            textBox4.Text = datos.Length > 4 ? datos[4].Trim() : string.Empty;
                            encontrado = true;

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
                    MessageBox.Show("No se encontró ningún registro con esa clave.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string claveBuscar = textBox1.Text.Trim();

            // Validar búsqueda
            if (!vtxt.ValidarBusqueda(claveBuscar))
                return;

            // Validar datos a actualizar
            if (!vtxt.validaCampos(textBox1.Text, txtPSW.Text, textBox2.Text, comboBox1.Text, textBox4.Text))
                return;

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            linea = textBox1.Text.Trim() + "," + textBox2.Text.Trim() + "," + txtPSW.Text.Trim() + "," + comboBox1.Text.Trim() + "," + textBox4.Text.Trim();
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

                    MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con esa clave.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string claveBuscar = textBox1.Text.Trim();

            // Validar búsqueda
            if (!vtxt.ValidarBusqueda(claveBuscar))
                return;

            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("No existe el archivo de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmación antes de eliminar
            DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este registro? Esta acción no se puede deshacer.", 
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado != DialogResult.Yes)
                return;

            bool encontrado = false;
            List<string> registros = new List<string>();

            try
            {
                using (StreamReader arch = new StreamReader(rutaArchivo))
                {
                    while ((linea = arch.ReadLine()) != null)
                    {
                        string[] datos = linea.Split(',');

                        if (datos.Length > 0 && datos[0].Trim() == claveBuscar)
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

                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con esa clave.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Este método ya está vinculado a btnSearch_Click en el constructor
            // No es necesario llamarlo nuevamente aquí
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

                            
                            if (!clavesVistas.Contains(clave))
                            {
                                registros.Add(linea);
                                clavesVistas.Add(clave);
                            }
                            else
                            {
                                
                                duplicadosEliminados++;
                            }
                        }
                    }
                }

                
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
        /// <summary>
        /// Valida que un ID no esté vacío
        /// </summary>
        public bool validaID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("El ID no puede estar vacío.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que un valor sea numérico
        /// </summary>
        private bool ValidarNumerico(string valor, string nombreCampo)
        {
            if (!decimal.TryParse(valor, out _))
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe contener solo números.", "Error de Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que un valor contenga solo letras y espacios
        /// </summary>
        private bool ValidarSoloLetras(string valor, string nombreCampo)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
            if (!regex.IsMatch(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' solo puede contener letras.", 
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que un valor no esté vacío
        /// </summary>
        private bool ValidarNoVacio(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida una contraseña con requisitos de seguridad
        /// Requisitos: mínimo 6 caracteres, al menos 1 número
        /// </summary>
        private bool ValidarContraseña(string contraseña, string nombreCampo)
        {
            if (!ValidarNoVacio(contraseña, nombreCampo)) return false;
            if (contraseña.Length < 6)
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe tener al menos 6 caracteres.", 
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\d");
            if (!regex.IsMatch(contraseña))
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe contener al menos un número.", 
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que un rango de números esté dentro de límites
        /// </summary>
        private bool ValidarRango(decimal valor, decimal minimo, decimal maximo, string nombreCampo)
        {
            if (valor < minimo || valor > maximo)
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe estar entre {minimo} y {maximo}.", 
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida todos los campos del formulario de Personal
        /// Parámetros: clave, contraseña, nombre, cargo, salario
        /// </summary>
        public bool validaCampos(string clave, string contraseña, string nombre, string cargo, string salario)
        {
            // Validar Clave (solo números, 3-5 dígitos)
            if (!ValidarNoVacio(clave, "Clave")) return false;
            if (!ValidarNumerico(clave, "Clave")) return false;
            if (clave.Length < 3 || clave.Length > 5)
            {
                MessageBox.Show("La clave debe tener entre 3 y 5 dígitos.", "Error de Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar Nombre (solo letras y espacios)
            if (!ValidarNoVacio(nombre, "Nombre")) return false;
            if (!ValidarSoloLetras(nombre, "Nombre")) return false;
            if (nombre.Length < 3)
            {
                MessageBox.Show("El nombre debe tener al menos 3 caracteres.", "Error de Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validar Contraseña (seguridad: mín 6 caracteres, debe tener números)
            if (!ValidarContraseña(contraseña, "Contraseña")) return false;

            // Validar Cargo (no vacío)
            if (!ValidarNoVacio(cargo, "Cargo")) return false;

            // Validar Salario (numérico, mayor a 0)
            if (!ValidarNoVacio(salario, "Salario")) return false;
            if (!ValidarNumerico(salario, "Salario")) return false;
            if (!decimal.TryParse(salario, out decimal salarioDecimal))
                return false;
            if (!ValidarRango(salarioDecimal, 0.01m, 999999.99m, "Salario")) return false;

            return true;
        }

        /// <summary>
        /// Valida campos para búsqueda (solo clave requerida)
        /// </summary>
        public bool ValidarBusqueda(string clave)
        {
            if (!ValidarNoVacio(clave, "Clave")) return false;
            if (!ValidarNumerico(clave, "Clave")) return false;
            return true;
        }
    }
}


