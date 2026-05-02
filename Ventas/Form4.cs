using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;

namespace Ventas
{
    public partial class FormVentas : Form
    {
        private readonly string rutaArticulos = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "VentasArticulos.txt");

        private readonly string rutaFacturas = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "facturas.txt");

        private decimal totalVenta = 0;

        public FormVentas()
        {
            InitializeComponent();

            // Conectar eventos de botones
            btnAgregar.Click += BtnAgregar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnRegistar.Click += BtnRegistrar_Click;

            // Evento para sincronizar ListBox
            listClave.SelectedIndexChanged += SincronizarListas;
            listDescripcion.SelectedIndexChanged += SincronizarListas;
            listUnidades.SelectedIndexChanged += SincronizarListas;
            listPrecio.SelectedIndexChanged += SincronizarListas;
            list_import.SelectedIndexChanged += SincronizarListas;

            // Cargar catálogo de productos
            CargarCatalogoProductos();
        }

        // ===============================
        // 📦 CARGAR CATÁLOGO DE PRODUCTOS
        // ===============================
        private void CargarCatalogoProductos()
        {
            try
            {
                if (!File.Exists(rutaArticulos))
                {
                    MessageBox.Show("El archivo de artículos no existe.\nSe creará uno nuevo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CrearArchivoEjemplo();
                    return;
                }

                int productosEncontrados = 0;

                foreach (var linea in File.ReadAllLines(rutaArticulos))
                {
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split(',');

                    // Formato: Clave,Descripción,Existencias,Costo,Precio
                    if (datos.Length >= 5)
                    {
                        string clave = datos[0].Trim();
                        string descripcion = datos[1].Trim();
                        string precio = datos[4].Trim();

                        productosEncontrados++;
                    }
                }

                MessageBox.Show($"Catálogo cargado: {productosEncontrados} productos disponibles.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar catálogo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===============================
        // 📝 CREAR ARCHIVO EJEMPLO
        // ===============================
        private void CrearArchivoEjemplo()
        {
            try
            {
                List<string> productos = new List<string>
                {
                    "P001,Laptop Dell,10,800.00,1200.00",
                    "P002,Mouse Logitech,50,15.00,25.00",
                    "P003,Teclado Mecánico,30,60.00,100.00",
                    "P004,Monitor LG 24\",15,200.00,350.00",
                    "P005,Cable HDMI,100,5.00,10.00"
                };

                File.WriteAllLines(rutaArticulos, productos);
                MessageBox.Show("Archivo de ejemplo creado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===============================
        // 🟢 AGREGAR PRODUCTO (CARRITO)
        // ===============================
        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            string clave = txbclave.Text.Trim();
            string descripcion = txbdescripcion.Text.Trim();
            string unidades = txbunidades.Text.Trim();
            string precio = txbprecio.Text.Trim();

            // Validación básica
            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(descripcion) || 
                string.IsNullOrEmpty(unidades) || string.IsNullOrEmpty(precio))
            {
                MessageBox.Show("Completa todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que sean números
            if (!int.TryParse(unidades, out int cant) || !decimal.TryParse(precio, out decimal pre))
            {
                MessageBox.Show("Unidades o precio inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cant <= 0 || pre <= 0)
            {
                MessageBox.Show("Unidades y precio deben ser mayores que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Agregar a los ListBox
                listClave.Items.Add(clave);
                listDescripcion.Items.Add(descripcion);
                listUnidades.Items.Add(cant);
                listPrecio.Items.Add(pre.ToString("F2"));

                decimal importe = cant * pre;
                list_import.Items.Add(importe.ToString("F2"));

                // Actualizar total
                totalVenta += importe;
                ActualizarTotal();

                // Limpiar campos
                LimpiarCampos();
                txbclave.Focus();

                MessageBox.Show("Producto agregado al carrito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===============================
        // 🔴 ELIMINAR PRODUCTO DEL CARRITO
        // ===============================
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            int index = listClave.SelectedIndex;

            if (index < 0)
            {
                MessageBox.Show("Selecciona un producto para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Restar del total
                if (decimal.TryParse(list_import.Items[index].ToString(), out decimal importe))
                {
                    totalVenta -= importe;
                }

                // Eliminar de todos los ListBox
                listClave.Items.RemoveAt(index);
                listDescripcion.Items.RemoveAt(index);
                listUnidades.Items.RemoveAt(index);
                listPrecio.Items.RemoveAt(index);
                list_import.Items.RemoveAt(index);

                ActualizarTotal();
                MessageBox.Show("Producto eliminado del carrito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===============================
        // 🧾 REGISTRAR / GENERAR FACTURA
        // ===============================
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (listClave.Items.Count == 0)
            {
                MessageBox.Show("No hay productos en el carrito.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string factura = "╔════════════════════════════════╗\n";
                factura += "║          FACTURA DE VENTA       ║\n";
                factura += "╚════════════════════════════════╝\n";
                factura += $"Fecha: {fecha}\n";
                factura += $"Factura #: {DateTime.Now.Ticks}\n";
                factura += "────────────────────────────────────\n";
                factura += String.Format("{0,-10} {1,-15} {2,-8} {3,-10} {4,-12}\n", 
                    "CLAVE", "DESCRIPCIÓN", "CANT", "PRECIO", "SUBTOTAL");
                factura += "────────────────────────────────────\n";

                int totalArticulos = 0;
                decimal totalFinal = 0;

                for (int i = 0; i < listClave.Items.Count; i++)
                {
                    string clave = listClave.Items[i].ToString();
                    string desc = listDescripcion.Items[i].ToString();
                    int cantidad = int.Parse(listUnidades.Items[i].ToString());
                    decimal precio = decimal.Parse(listPrecio.Items[i].ToString());
                    decimal subtotal = decimal.Parse(list_import.Items[i].ToString());

                    factura += String.Format("{0,-10} {1,-15} {2,-8} ${3,-9:F2} ${4,-10:F2}\n",
                        clave, desc.Substring(0, Math.Min(15, desc.Length)), cantidad, precio, subtotal);

                    totalArticulos += cantidad;
                    totalFinal += subtotal;
                }

                factura += "────────────────────────────────────\n";
                factura += $"Total Artículos: {totalArticulos}\n";
                factura += $"TOTAL A PAGAR:   ${totalFinal:F2}\n";
                factura += "════════════════════════════════════\n\n";

                // Guardar en archivo
                File.AppendAllText(rutaFacturas, factura);

                MessageBox.Show($"Factura generada correctamente.\n\nTotal: ${totalFinal:F2}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===============================
        // 🔁 SINCRONIZAR LISTBOX
        // ===============================
        private void SincronizarListas(object sender, EventArgs e)
        {
            ListBox listaOrigen = (ListBox)sender;
            int indice = listaOrigen.SelectedIndex;

            if (indice >= 0)
            {
                listClave.SelectedIndex = indice;
                listDescripcion.SelectedIndex = indice;
                listUnidades.SelectedIndex = indice;
                listPrecio.SelectedIndex = indice;
                list_import.SelectedIndex = indice;
            }
        }

        // ===============================
        // 💰 ACTUALIZAR TOTAL
        // ===============================
        private void ActualizarTotal()
        {
            txb_importe.Text = totalVenta.ToString("F2");
        }

        // ===============================
        // 🧹 LIMPIAR CAMPOS DE ENTRADA
        // ===============================
        private void LimpiarCampos()
        {
            txbclave.Clear();
            txbdescripcion.Clear();
            txbunidades.Clear();
            txbprecio.Clear();
        }

        // ===============================
        // 🧹 LIMPIAR TODO (CARRITO)
        // ===============================
        private void LimpiarTodo()
        {
            listClave.Items.Clear();
            listDescripcion.Items.Clear();
            listUnidades.Items.Clear();
            listPrecio.Items.Clear();
            list_import.Items.Clear();

            LimpiarCampos();
            totalVenta = 0;
            ActualizarTotal();

            txbclave.Focus();
        }
    }
}

