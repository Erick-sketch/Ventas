# 📄 ESTRUCTURA DEL ARCHIVO VentasPersonal.txt

## 📌 UBICACIÓN DEL ARCHIVO

```
📁 C:\Users\{NombreUsuario}\Documents\
└── VentasPersonal.txt
```

**Ruta completa en el código:**
```csharp
Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
             "VentasPersonal.txt")
```

---

## 🎯 FORMATO DEL ARCHIVO

### ESTRUCTURA DE LÍNEA:
```
Clave,Nombre,Contraseña,Cargo,Salario
```

### SEPARADOR:
- **Carácter:** Coma (`,`)
- **Importante:** No usar comas en los valores

### CAMPOS:

| Campo | Tipo | Longitud | Ejemplo | Validación |
|-------|------|----------|---------|-----------|
| **Clave** | Texto | 2-10 | P001 | No vacío, único |
| **Nombre** | Texto | 3-50 | Juan Pérez | No vacío, letras/espacios |
| **Contraseña** | Texto | 6-20 | Pass123 | Mínimo 6 caracteres |
| **Cargo** | Combo | - | Venta | Venta o Administrador |
| **Salario** | Decimal | - | 25000.00 | > 0, formato XX.XX |

---

## 📋 EJEMPLO COMPLETO

### Archivo VentasPersonal.txt (Mínimo):

```
P001,Juan Pérez,Pass123,Venta,25000.00
P002,María García,Admin456,Administrador,35000.00
P003,Carlos López,Vend789,Venta,24000.00
```

### Archivo VentasPersonal.txt (Completo):

```
P001,Juan Pérez,Pass123,Venta,25000.00
P002,María García,Admin456,Administrador,35000.00
P003,Carlos López,Vend789,Venta,24000.00
P004,Ana Martínez,Admin123,Administrador,36000.00
P005,Roberto Silva,Vend456,Venta,23500.00
P006,Laura González,Pass987,Venta,25500.00
P007,Diego Hernández,Admin789,Administrador,37000.00
P008,Sofía Rodríguez,Vend123,Venta,24500.00
```

---

## ✅ VALIDACIONES POR CAMPO

### 1. CLAVE (Primera columna)
```
✅ VÁLIDAS:
- P001
- ADMIN1
- V-002
- EMP_003

❌ INVÁLIDAS:
- (vacía)
- P001,Ejemplo (contiene coma)
- " " (solo espacios)

REGLA: Único, no repetido, no vacío
```

### 2. NOMBRE (Segunda columna)
```
✅ VÁLIDAS:
- Juan Pérez
- María del Carmen
- Jose Rodriguez

❌ INVÁLIDAS:
- (vacía)
- J (menos de 3 caracteres)
- Juan,Extra (contiene coma)

REGLA: Mínimo 3 caracteres, sin números
```

### 3. CONTRASEÑA (Tercera columna)
```
✅ VÁLIDAS:
- Pass123
- Segura456
- Admin@789

❌ INVÁLIDAS:
- Pass (menos de 6 caracteres)
- P,123 (contiene coma)
- (vacía)

REGLA: Mínimo 6 caracteres, alfanumérico
```

### 4. CARGO (Cuarta columna)
```
✅ VÁLIDAS:
- Venta
- Administrador

❌ INVÁLIDAS:
- Gerente (no está en lista)
- venta (minúscula, debe ser exacta)
- Venta,Administrador (contiene coma)
- (vacía)

REGLA: Solo "Venta" o "Administrador"
```

### 5. SALARIO (Quinta columna)
```
✅ VÁLIDAS:
- 25000.00
- 35000.50
- 1000.00

❌ INVÁLIDAS:
- 500 (menor a 1000)
- -5000 (negativo)
- 25.000,00 (formato incorrecto)
- Veinticinco mil (texto)
- (vacía)

REGLA: Decimal > 1000, formato XX.XX
```

---

## 🔄 FLUJO DE DATOS

### Lectura:
```
VentasPersonal.txt
        ↓
File.ReadAllLines()
        ↓
Split(',')
        ↓
Llenar campos en Form1
        ↓
Mostrar en ListBox
```

### Escritura:
```
Capturar datos en Form1
        ↓
Validar con ValidarDatos()
        ↓
Construir línea: "P001,Juan,Pass123,Venta,25000.00"
        ↓
File.AppendAllText() o StreamWriter
        ↓
Guardar en VentasPersonal.txt
```

### Actualización:
```
Leer archivo completo
        ↓
Encontrar línea con clave igual
        ↓
Reemplazar línea
        ↓
Guardar archivo
```

### Eliminación:
```
Leer archivo completo
        ↓
Eliminar línea con clave igual
        ↓
Guardar archivo sin esa línea
```

---

## 📊 EJEMPLO DE OPERACIONES

### AGREGAR NUEVO PERSONAL:
```
Entrada:
- Clave: P009
- Nombre: Pedro González
- Contraseña: NewPass123
- Cargo: Administrador
- Salario: 38000.00

Línea generada:
P009,Pedro González,NewPass123,Administrador,38000.00

Acción:
File.AppendAllText(rutaArchivo, "P009,Pedro González,NewPass123,Administrador,38000.00\n")
```

### BUSCAR PERSONAL:
```
Buscar: P003

Línea encontrada:
P003,Carlos López,Vend789,Venta,24000.00

Split(','):
[0] = P003
[1] = Carlos López
[2] = Vend789
[3] = Venta
[4] = 24000.00

Mostrar en campos:
- textBox1.Text = P003
- textBox2.Text = Carlos López
- txtPSW.Text = Vend789
- comboBox1.Text = Venta
- textBox4.Text = 24000.00
```

### ACTUALIZAR SALARIO:
```
Buscar: P001

Línea actual:
P001,Juan Pérez,Pass123,Venta,25000.00

Nuevo salario: 26000.00

Nueva línea:
P001,Juan Pérez,Pass123,Venta,26000.00

Guardar archivo actualizado
```

### ELIMINAR PERSONAL:
```
Eliminar: P005

Archivo antes:
P001,Juan Pérez,...
P002,María García,...
P003,Carlos López,...
P004,Ana Martínez,...
P005,Roberto Silva,...      ← Eliminar esta
P006,Laura González,...
P007,Diego Hernández,...

Archivo después:
P001,Juan Pérez,...
P002,María García,...
P003,Carlos López,...
P004,Ana Martínez,...
P006,Laura González,...
P007,Diego Hernández,...
```

### LIMPIAR DUPLICADOS:
```
Archivo con duplicados:
P001,Juan Pérez,Pass123,Venta,25000.00
P002,María García,Admin456,Administrador,35000.00
P001,Juan Pérez,Pass123,Venta,25000.00    ← DUPLICADO
P003,Carlos López,Vend789,Venta,24000.00
P002,María García,Admin456,Administrador,35000.00    ← DUPLICADO

Resultado limpio:
P001,Juan Pérez,Pass123,Venta,25000.00
P002,María García,Admin456,Administrador,35000.00
P003,Carlos López,Vend789,Venta,24000.00
```

---

## 🛡️ PROBLEMAS COMUNES

### PROBLEMA 1: Comas en los valores
```
❌ INCORRECTO:
P001,García, Juan,Pass123,Venta,25000.00
         ↑ Coma extra

✅ CORRECTO:
P001,García Juan,Pass123,Venta,25000.00
```

### PROBLEMA 2: Formato de salario
```
❌ INCORRECTO:
P001,Juan,Pass,Venta,25000
                        ↑ Sin decimales

P001,Juan,Pass,Venta,25.000,00
                        ↑ Formato europeo

✅ CORRECTO:
P001,Juan,Pass,Venta,25000.00
```

### PROBLEMA 3: Caracteres especiales
```
❌ INCORRECTO:
P001,Juan Pérez,Paß123,Venta,25000.00
                  ↑ Carácter especial en contraseña

P001,José María,Pass123,Venta,25000.00
    ↑ Acentos pueden causar problemas

✅ CORRECTO:
P001,Juan Perez,Pass123,Venta,25000.00
P001,Jose Maria,Pass123,Venta,25000.00
```

### PROBLEMA 4: Línea en blanco
```
❌ INCORRECTO:
P001,Juan,Pass123,Venta,25000.00
                                    ← Línea vacía
P002,María,Pass456,Admin,35000.00

✅ CORRECTO:
P001,Juan,Pass123,Venta,25000.00
P002,María,Pass456,Admin,35000.00
```

---

## 📝 CÓDIGO PARA CREAR ARCHIVO DE EJEMPLO

```csharp
private void CrearArchivoEjemplo()
{
    try
    {
        List<string> personal = new List<string>
        {
            "P001,Juan Pérez,Pass123,Venta,25000.00",
            "P002,María García,Admin456,Administrador,35000.00",
            "P003,Carlos López,Vend789,Venta,24000.00",
            "P004,Ana Martínez,Admin123,Administrador,36000.00",
            "P005,Roberto Silva,Vend456,Venta,23500.00",
            "P006,Laura González,Pass987,Venta,25500.00",
            "P007,Diego Hernández,Admin789,Administrador,37000.00",
            "P008,Sofía Rodríguez,Vend123,Venta,24500.00"
        };

        File.WriteAllLines(rutaArchivo, personal);
        MessageBox.Show("Archivo de ejemplo creado.", "Éxito");
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }
}
```

---

## 🔍 VERIFICAR INTEGRIDAD DEL ARCHIVO

```csharp
private bool VerificarArchivo()
{
    if (!File.Exists(rutaArchivo))
    {
        MessageBox.Show("Archivo no existe.");
        return false;
    }

    try
    {
        foreach (var linea in File.ReadAllLines(rutaArchivo))
        {
            if (string.IsNullOrWhiteSpace(linea))
                continue;

            var datos = linea.Split(',');

            if (datos.Length != 5)
            {
                MessageBox.Show($"Línea inválida: {linea}");
                return false;
            }

            // Validar cada campo
            if (string.IsNullOrEmpty(datos[0])) return false;  // Clave
            if (string.IsNullOrEmpty(datos[1])) return false;  // Nombre
            if (string.IsNullOrEmpty(datos[2])) return false;  // Contraseña
            if (string.IsNullOrEmpty(datos[3])) return false;  // Cargo
            if (!decimal.TryParse(datos[4], out decimal sal) || sal <= 0) 
                return false;  // Salario
        }

        return true;
    }
    catch
    {
        return false;
    }
}
```

---

## 📊 ESTADÍSTICAS DEL ARCHIVO

```
Línea por personal: 1
Separador: Coma (,)
Campos por línea: 5
Longitud aproximada por línea: 60-80 caracteres

Ejemplo con 8 personas:
- Líneas: 8
- Tamaño: ~500 bytes
- Encoding: UTF-8 recomendado
```

---

## 🔧 MANTENIMIENTO DEL ARCHIVO

### Respaldar:
```csharp
private void RespaldarArchivo()
{
    if (File.Exists(rutaArchivo))
    {
        string respaldo = rutaArchivo + ".bak";
        File.Copy(rutaArchivo, respaldo, true);
    }
}
```

### Limpiar:
```csharp
private void LimpiarArchivoVacio()
{
    if (File.Exists(rutaArchivo))
    {
        var lineas = File.ReadAllLines(rutaArchivo)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        File.WriteAllLines(rutaArchivo, lineas);
    }
}
```

### Validar:
```csharp
private int ContarRegistros()
{
    if (!File.Exists(rutaArchivo)) return 0;

    return File.ReadAllLines(rutaArchivo)
        .Where(l => !string.IsNullOrWhiteSpace(l))
        .Count();
}
```

---

## 📌 RESUMEN

| Aspecto | Valor |
|---------|-------|
| **Ubicación** | Documents/VentasPersonal.txt |
| **Formato** | CSV (Comma Separated Values) |
| **Separador** | Coma (,) |
| **Campos** | 5 (Clave, Nombre, Pass, Cargo, Salario) |
| **Encoding** | UTF-8 |
| **Línea vacía** | NO permitida |
| **Duplicados** | NO permitidos |
| **Comentarios** | NO soportados |

---

## ✅ CHECKLIST

Al crear/actualizar el archivo:

```
☐ Archivo existe en Documents/
☐ Formato CSV correcto (comas)
☐ 5 campos por línea
☐ Sin líneas vacías
☐ Sin duplicados
☐ Clave única
☐ Salario > 1000
☐ Cargo válido (Venta o Admin)
☐ Contraseña >= 6 caracteres
☐ Nombre >= 3 caracteres
☐ Encoding UTF-8
☐ Archivo legible por el programa
```

