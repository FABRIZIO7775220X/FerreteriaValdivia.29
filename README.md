'''# Ferretería Valdivia — Sistema de Gestión (ASP.NET Core MVC + MariaDB / XAMPP)

Aplicación web desarrollada en **ASP.NET Core MVC** con **Entity Framework Core** y base de datos relacional en **MariaDB** (ejecutada mediante **XAMPP**).

---

## 1. Requisitos previos (qué descargar e instalar en la otra máquina)

1. **SDK de .NET** (actualmente el proyecto está configurado en **.NET 10**, o **.NET 9 SDK** si vas a bajar el *Target Framework* a `net9.0` como se explica en la Sección 7):
   - Verifica en terminal con:
     ```bash
     dotnet --version
     ```
2. **XAMPP** (incluye servidor **Apache** y **MariaDB / MySQL**):
   - Necesitarás iniciar el módulo de **MySQL** (MariaDB) desde el panel de control de XAMPP.
3. **Editor / IDE** (recomendado):
   - **Visual Studio Code** (con extensión de C# / C# Dev Kit) o **Visual Studio**.
   - **DBeaver** o **phpMyAdmin** (incluido en XAMPP) para revisar tablas, datos y consultas.
4. **Git** (para clonar o actualizar el repositorio `ferreteriavaldiviaprograS2`):
   - Verifica con:
     ```bash
     git --version
     ```

---

## 2. Qué abrir y qué activar antes de ejecutar

1. **Abre XAMPP Control Panel** (como Administrador si tu sistema lo requiere).
2. **Inicia (`Start`) el servicio `MySQL`** en XAMPP.
   - Verifica que el indicador quede en verde y que el puerto por defecto (`3306`) esté activo sin conflictos.
   - *(Opcional)* Haz clic en **Admin** junto a `MySQL` para abrir **phpMyAdmin** y comprobar que el servidor responde.
3. **Abre la carpeta del proyecto** (`FerreteriaValdivia.29`) en **Visual Studio Code** o abre una terminal directamente en la ruta de la carpeta raíz del proyecto.

---

## 3. Cómo funciona y cómo configurar la base de datos (`script_mariadb.sql`)

El archivo **`script_mariadb.sql`** (ubicado en la **raíz del proyecto**) crea la base de datos `FerreteriaValdivia` y todas sus tablas para **MariaDB / XAMPP** sin bloques `BEGIN ... END`, sin la tabla `Usuario` y con las restricciones de integridad listas.

### Orden lógico de tablas y dependencias
El modelo sigue esta jerarquía para respetar las claves foráneas (*Foreign Keys*):

1. **Catálogos base:**
   - `Rol`
   - `Categoria`
   - `Proveedor`
   - `Cliente`
2. **Entidades dependientes de catálogos:**
   - `Empleado` (se relaciona con `Rol`)
   - `Caja` (se relaciona con `Empleado`)
   - `Productos` (se relaciona con `Categoria` y `Proveedor`)
3. **Flujo transaccional:**
   - `Venta` (se relaciona con `Cliente`, `Empleado` y `Caja`)
   - `DetalleVenta` (se relaciona con `Venta` y `Productos`)
   - `Pago` (se relaciona con `Venta`)

> **Integridad de datos:** Las relaciones históricas usan restricciones como `RESTRICT` o `SET NULL` donde corresponde para evitar borrar por accidente registros históricos de ventas o cajas al eliminar catálogos o personas relacionadas.

### Pasos para crear la base de datos en XAMPP
1. Asegúrate de que **MySQL** esté iniciado en **XAMPP**.
2. Ejecuta **`script_mariadb.sql`**:
   - **Desde phpMyAdmin:** abre `http://localhost/phpmyadmin`, entra a la pestaña **SQL**, pega el contenido de `script_mariadb.sql` y haz clic en **Continuar**.
   - **Desde terminal / cliente MariaDB:**
     ```bash
     mysql -u root -p < script_mariadb.sql
     ```
     *(Si `root` no tiene contraseña en XAMPP, presiona `Enter` al pedir la contraseña).*
3. Confirma que la base de datos **`FerreteriaValdivia`** se haya creado y contenga todas las tablas (incluida `__EFMigrationsHistory`).

---

## 4. Configurar la cadena de conexión (`appsettings.json`)

Revisa el archivo **`appsettings.json`** (en la raíz del proyecto, y **`appsettings.Development.json`** si aplica) para asegurarte de que `DefaultConnection` coincida con el MariaDB de XAMPP en esa máquina:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=FerreteriaValdivia;user=root;password=;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

* Ajusta `server`, `port`, `database`, `user` o `password` si en esa computadora XAMPP está configurado con otros parámetros.

---

## 5. Cómo restaurar, compilar y ejecutar el proyecto

Abre una terminal en la **raíz del proyecto** (`FerreteriaValdivia.29`) y ejecuta en orden:

1. **Restaurar paquetes NuGet:**
```bash
dotnet restore

```


*Verificación:* Debe descargar/validar los paquetes sin errores ni conflictos de versión.
2. **Compilar el proyecto:**
```bash
dotnet build

```


*Verificación:* Debe finalizar con **0 errores** y **0 advertencias**.
3. **Ejecutar la aplicación:**
```bash
dotnet run

```


*Verificación:* La terminal mostrará las direcciones locales (por ejemplo `http://localhost:5xxx` o `https://localhost:7xxx`). Ábrelas en tu navegador.

---

## 6. Flujo recomendado para usar y probar la aplicación

Sigue este orden al cargar o probar registros desde la interfaz web para mantener coherencia en las dependencias:

1. **Roles**
2. **Categorías**
3. **Proveedores**
4. **Clientes**
5. **Cajas**
6. **Empleados**
7. **Productos**
8. **Ventas**
9. **Detalle Ventas**
10. **Pagos**

---

## 7. Cambiar de .NET 10 a .NET 9 (si la otra máquina usa .NET 9)

El proyecto se mantiene en **.NET 10**. Si en la otra máquina solo tienes instalado **.NET 9 SDK** (o necesitas compilar apuntando a `net9.0`), edita el archivo `FerreteriaValdivia.csproj` que está directamente en la **raíz del proyecto** (no está dentro de ninguna subcarpeta como `Controllers`, `Models` o `Views`):

1. Abre `FerreteriaValdivia.csproj` en la raíz del proyecto.
2. Cambia `<TargetFramework>` de `net10.0` a `net9.0`:
```xml
<PropertyGroup>
  <TargetFramework>net9.0</TargetFramework>
  <!-- demás propiedades -->
</PropertyGroup>

```


3. Si las líneas `<PackageReference ... />` tienen versiones `10.x.x` (por ejemplo de `Microsoft.EntityFrameworkCore.*`), cámbialas a una versión `9.0.x` (por ejemplo `9.0.0`) compatible con .NET 9.
4. Guarda `FerreteriaValdivia.csproj` y ejecuta en la terminal:
```bash
dotnet restore
dotnet build

```



---

## 8. Solución rápida a problemas comunes

* **Error de conexión a MySQL / MariaDB:** Verifica que **MySQL** esté corriendo en **XAMPP**, que el puerto (`3306`) no esté ocupado y que los datos en `appsettings.json` coincidan.
* **No existen la base de datos o las tablas:** Ejecuta nuevamente `script_mariadb.sql` en MariaDB / XAMPP.
* **Error de SDK o versión de framework:** Instala el SDK correspondiente o aplica los pasos de la **Sección 7** en `FerreteriaValdivia.csproj` para usar `net9.0`.
* **Error de clave foránea al crear registros:** Asegúrate de haber creado previamente los registros padres requeridos (por ejemplo, `Empleado` antes de `Caja`, y `Cliente`, `Empleado` y `Caja` antes de `Venta`).

```