# 💻 CÓDIGO DE REFERENCIA - Form1 Mejoras

## 1️⃣ AGREGAR ListBox en Designer.cs

```csharp
// En InitializeComponent() agregar:

listPersonal = new ListBox();
listPersonal.FormattingEnabled = true;
listPersonal.Location = new Point(450, 80);
listPersonal.Name = "listPersonal";
listPersonal.Size = new Size(350, 250);
listPersonal.TabIndex = 20;
listPersonal.SelectedIndexChanged += ListPersonal_SelectedIndexChanged;

// Label para mostrar total
labelTotal = new Label();
labelTotal.AutoSize = true;
labelTotal.Location = new Point(450, 350);
labelTotal.Name = "labelTotal";
labelTotal.Text = "Total: 0 registros";

// Agregar a GroupBox
groupBox1.Controls.Add(listPersonal);
groupBox1.Controls.Add(labelTotal);
```

---

## 2️⃣ MÉTODO CargarPersonal() - Form1.cs

```csharp
private void CargarPersonal()
{
    try
    {
        if (!File.Exists(rutaArchivo))
        {
            MessageBox.Show("El archivo de personal no existe. Se creará uno nuevo.", 
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        ActualizarListadoPersonal();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al cargar personal: " + ex.Message, "Error");
    }
}

private void ActualizarListadoPersonal()
{
    try
    {
        listPersonal.Items.Clear();
        int totalPersonal = 0;

        if (File.Exists(rutaArchivo))
        {
            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;

                var datos = linea.Split(',');
                if (datos.Length >= 4)
                {
                    string clave = datos[0].Trim();
                    string nombre = datos[1].Trim();
                    string cargo = datos[3].Trim();

                    string item = $"{clave} - {nombre} ({cargo})";
                    listPersonal.Items.Add(item);
                    totalPersonal++;
                }
            }
        }

        labelTotal.Text = $"Total: {totalPersonal} registros";
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al actualizar listado: " + ex.Message);
    }
}
```

---

## 3️⃣ VALIDACIÓN COMPLETA - Form1.cs

```csharp
private bool ValidarDatos()
{
    string clave = textBox1.Text.Trim();
    string nombre = textBox2.Text.Trim();
    string contrasena = txtPSW.Text.Trim();
    string cargo = comboBox1.Text.Trim();
    string salario = textBox4.Text.Trim();

    // Validar Clave
    if (string.IsNullOrEmpty(clave))
    {
        MessageBox.Show("La clave no puede estar vacía.", "Validación");
        return false;
    }

    if (clave.Length < 2)
    {
        MessageBox.Show("La clave debe tener al menos 2 caracteres.", "Validación");
        return false;
    }

    // Validar Nombre
    if (string.IsNullOrEmpty(nombre))
    {
        MessageBox.Show("El nombre no puede estar vacío.", "Validación");
        return false;
    }

    if (nombre.Length < 3)
    {
        MessageBox.Show("El nombre debe tener al menos 3 caracteres.", "Validación");
        return false;
    }

    // Validar Contraseña
    if (string.IsNullOrEmpty(contrasena))
    {
        MessageBox.Show("La contraseña no puede estar vacía.", "Validación");
        return false;
    }

    if (contrasena.Length < 6)
    {
        MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Validación");
        return false;
    }

    // Validar Cargo
    if (string.IsNullOrEmpty(cargo))
    {
        MessageBox.Show("Debe seleccionar un cargo.", "Validación");
        return false;
    }

    if (cargo != "Administrador" && cargo != "Venta")
    {
        MessageBox.Show("Cargo inválido. Seleccione 'Administrador' o 'Venta'.", "Validación");
        return false;
    }

    // Validar Salario
    if (string.IsNullOrEmpty(salario))
    {
        MessageBox.Show("El salario no puede estar vacío.", "Validación");
        return false;
    }

    if (!decimal.TryParse(salario, out decimal salarioNum) || salarioNum <= 0)
    {
        MessageBox.Show("El salario debe ser un número válido mayor a cero.", "Validación");
        return false;
    }

    if (salarioNum < 1000)
    {
        MessageBox.Show("El salario no puede ser menor a 1000.", "Validación");
        return false;
    }

    return true;
}

private bool ExistePersona(string clave)
{
    if (!File.Exists(rutaArchivo))
        return false;

    try
    {
        foreach (var linea in File.ReadAllLines(rutaArchivo))
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            var datos = linea.Split(',');
            if (datos.Length > 0 && datos[0].Trim().ToLower() == clave.ToLower())
                return true;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al validar: " + ex.Message);
    }

    return false;
}
```

---

## 4️⃣ LIMPIAR DUPLICADOS - Form1.cs

```csharp
private void btnLimpiarDuplicados_Click(object sender, EventArgs e)
{
    if (!File.Exists(rutaArchivo))
    {
        MessageBox.Show("No existe archivo de personal.", "Información");
        return;
    }

    try
    {
        List<string> registros = new List<string>();
        HashSet<string> clavesVistas = new HashSet<string>();
        int duplicadosEncontrados = 0;

        // Leer archivo
        foreach (var linea in File.ReadAllLines(rutaArchivo))
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            var datos = linea.Split(',');
            if (datos.Length > 0)
            {
                string clave = datos[0].Trim();

                // Si es la primera vez que vemos esta clave, guardar
                if (!clavesVistas.Contains(clave))
                {
                    registros.Add(linea);
                    clavesVistas.Add(clave);
                }
                else
                {
                    // Es un duplicado, no guardar
                    duplicadosEncontrados++;
                }
            }
        }

        // Guardar archivo limpio
        File.WriteAllLines(rutaArchivo, registros);

        MessageBox.Show(
            $"Limpieza completada.\n" +
            $"Duplicados eliminados: {duplicadosEncontrados}\n" +
            $"Registros válidos: {registros.Count}",
            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Recargar UI
        ActualizarListadoPersonal();
        LimpiarCampos();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al limpiar duplicados: " + ex.Message, "Error");
    }
}
```

---

## 5️⃣ SELECCIONAR DESDE ListBox - Form1.cs

```csharp
private void ListPersonal_SelectedIndexChanged(object sender, EventArgs e)
{
    if (listPersonal.SelectedIndex < 0)
        return;

    try
    {
        int indiceSeleccionado = listPersonal.SelectedIndex;
        var lineas = File.ReadAllLines(rutaArchivo);

        int contador = 0;
        foreach (var linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            if (contador == indiceSeleccionado)
            {
                var datos = linea.Split(',');
                if (datos.Length >= 5)
                {
                    textBox1.Text = datos[0].Trim();
                    textBox2.Text = datos[1].Trim();
                    txtPSW.Text = datos[2].Trim();
                    comboBox1.Text = datos[3].Trim();
                    textBox4.Text = datos[4].Trim();
                }
                break;
            }

            contador++;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
}
```

---

## 6️⃣ MEJORAR btnAdd_Click - Form1.cs

```csharp
private void btnAdd_Click(object sender, EventArgs e)
{
    // Validar todos los datos
    if (!ValidarDatos())
        return;

    string clave = textBox1.Text.Trim();

    // Verificar si ya existe
    if (ExistePersona(clave))
    {
        MessageBox.Show($"Ya existe una persona con clave '{clave}'.", "Validación");
        return;
    }

    try
    {
        string registro = $"{textBox1.Text.Trim()}," +
                         $"{textBox2.Text.Trim()}," +
                         $"{txtPSW.Text.Trim()}," +
                         $"{comboBox1.Text.Trim()}," +
                         $"{textBox4.Text.Trim()}";

        using (StreamWriter arch = new StreamWriter(rutaArchivo, true))
        {
            arch.WriteLine(registro);
        }

        MessageBox.Show("Registro agregado correctamente.", "Éxito");
        LimpiarCampos();
        ActualizarListadoPersonal();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al escribir: " + ex.Message);
    }
}
```

---

## 7️⃣ BÚSQUEDA AVANZADA - Form1.cs (NUEVO)

```csharp
private void BuscarPersonal(string tipo, string valor)
{
    if (string.IsNullOrEmpty(valor))
    {
        MessageBox.Show("Ingresa un valor para buscar.", "Validación");
        return;
    }

    if (!File.Exists(rutaArchivo))
    {
        MessageBox.Show("No existe archivo de personal.", "Información");
        return;
    }

    try
    {
        List<string[]> resultados = new List<string[]>();

        foreach (var linea in File.ReadAllLines(rutaArchivo))
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            var datos = linea.Split(',');

            bool coincide = false;

            switch (tipo.ToLower())
            {
                case "clave":
                    coincide = datos.Length > 0 && 
                               datos[0].Trim().ToLower().Contains(valor.ToLower());
                    break;
                case "nombre":
                    coincide = datos.Length > 1 && 
                               datos[1].Trim().ToLower().Contains(valor.ToLower());
                    break;
                case "cargo":
                    coincide = datos.Length > 3 && 
                               datos[3].Trim().ToLower() == valor.ToLower();
                    break;
            }

            if (coincide)
                resultados.Add(datos);
        }

        if (resultados.Count == 0)
        {
            MessageBox.Show($"No se encontraron resultados para '{valor}'.", "Búsqueda");
            return;
        }

        if (resultados.Count == 1)
        {
            var dato = resultados[0];
            textBox1.Text = dato[0].Trim();
            textBox2.Text = dato.Length > 1 ? dato[1].Trim() : "";
            txtPSW.Text = dato.Length > 2 ? dato[2].Trim() : "";
            comboBox1.Text = dato.Length > 3 ? dato[3].Trim() : "";
            textBox4.Text = dato.Length > 4 ? dato[4].Trim() : "";
        }
        else
        {
            string mensaje = $"Se encontraron {resultados.Count} resultados:\n\n";
            foreach (var dato in resultados)
            {
                mensaje += $"• {dato[0]} - {dato[1]} ({dato[3]})\n";
            }
            MessageBox.Show(mensaje, "Resultados");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error en búsqueda: " + ex.Message);
    }
}
```

---

## 8️⃣ CONSTRUCTOR MEJORADO - Form1.cs

```csharp
public Personal()
{
    InitializeComponent();

    // Conexión de eventos
    button1.Click += btnAdd_Click;
    button2.Click += btnSearch_Click;
    button3.Click += btnUpdate_Click;
    button4.Click += btnDelete_Click;
    btnLimpiarDuplicados.Click += btnLimpiarDuplicados_Click;
    listPersonal.SelectedIndexChanged += ListPersonal_SelectedIndexChanged;

    // Cargar datos al iniciar
    CargarPersonal();
}
```

---

## 📋 CHECKLIST DE IMPLEMENTACIÓN

```
[ ] Agregar ListBox "listPersonal" en Designer
[ ] Agregar Label "labelTotal" en Designer
[ ] Implementar CargarPersonal()
[ ] Implementar ActualizarListadoPersonal()
[ ] Implementar ValidarDatos()
[ ] Implementar ExistePersona()
[ ] Implementar btnLimpiarDuplicados_Click()
[ ] Implementar ListPersonal_SelectedIndexChanged()
[ ] Mejorar btnAdd_Click()
[ ] Mejorar btnSearch_Click()
[ ] Mejorar btnUpdate_Click()
[ ] Mejorar btnDelete_Click()
[ ] Actualizar Constructor
[ ] Probar todas las funciones
[ ] Validar archivo VentasPersonal.txt
```

---

## 🧪 PRUEBAS SUGERIDAS

```
1. Agregar 3 personas
   ✓ Verificar en ListBox
   ✓ Verificar total

2. Buscar por clave
   ✓ Debe cargar en campos

3. Actualizar persona
   ✓ Cambiar nombre
   ✓ Verificar cambio

4. Intentar agregar duplicado
   ✓ Debe rechazar

5. Limpiar duplicados
   ✓ Debe eliminar duplicados

6. Buscar por nombre
   ✓ Debe encontrar

7. Eliminar persona
   ✓ Debe desaparecer de ListBox
```

