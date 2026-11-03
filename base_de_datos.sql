-- 1. Crear y seleccionar la base de datos
CREATE DATABASE IF NOT EXISTS ferreteriavaldiviadb;
USE ferreteriavaldiviadb;

-- Desactivar temporalmente la verificación de llaves foráneas para evitar problemas de orden al crear
SET FOREIGN_KEY_CHECKS = 0;

-- 2. Tabla Categoria
CREATE TABLE IF NOT EXISTS Categoria (
    id_Categoria INT AUTO_INCREMENT PRIMARY KEY,
    nombre_Categoria VARCHAR(100) NOT NULL
);

-- 3. Tabla Proveedor
CREATE TABLE IF NOT EXISTS Proveedor (
    id_Proveedor INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    ubicacion VARCHAR(200)
);

-- 4. Tabla Cliente
CREATE TABLE IF NOT EXISTS Cliente (
    id_Cliente INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    direccion VARCHAR(200)
);

-- 5. Tabla Empleado
CREATE TABLE IF NOT EXISTS Empleado (
    id_Empleado INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    cargo VARCHAR(50),
    telefono VARCHAR(20)
);

-- 6. Tabla Producto (Depende de Categoria y Proveedor)
CREATE TABLE IF NOT EXISTS Producto (
    id_Producto INT AUTO_INCREMENT PRIMARY KEY,
    nombre_Producto VARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    marca VARCHAR(50),
    id_Categoria INT NOT NULL,
    id_Proveedor INT NOT NULL,
    CONSTRAINT fk_producto_categoria FOREIGN KEY (id_Categoria) REFERENCES Categoria(id_Categoria),
    CONSTRAINT fk_producto_proveedor FOREIGN KEY (id_Proveedor) REFERENCES Proveedor(id_Proveedor)
);

-- 7. Tabla Venta (Depende de Cliente y Empleado)
CREATE TABLE IF NOT EXISTS Venta (
    id_Venta INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    id_Cliente INT NOT NULL,
    id_Empleado INT NOT NULL,
    CONSTRAINT fk_venta_cliente FOREIGN KEY (id_Cliente) REFERENCES Cliente(id_Cliente),
    CONSTRAINT fk_venta_empleado FOREIGN KEY (id_Empleado) REFERENCES Empleado(id_Empleado)
);

-- 8. Tabla Detalle_Venta (Depende de Venta y Producto)
CREATE TABLE IF NOT EXISTS Detalle_Venta (
    id_Detalle INT AUTO_INCREMENT PRIMARY KEY,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    id_Venta INT NOT NULL,
    id_Producto INT NOT NULL,
    CONSTRAINT fk_detalleventa_venta FOREIGN KEY (id_Venta) REFERENCES Venta(id_Venta),
    CONSTRAINT fk_detalleventa_producto FOREIGN KEY (id_Producto) REFERENCES Producto(id_Producto)
);

-- 9. Tabla Pago (Depende de Venta)
CREATE TABLE IF NOT EXISTS Pago (
    id_Pago INT AUTO_INCREMENT PRIMARY KEY,
    monto DECIMAL(10,2) NOT NULL,
    metodo_pago VARCHAR(50) NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    id_Venta INT NOT NULL,
    CONSTRAINT fk_pago_venta FOREIGN KEY (id_Venta) REFERENCES Venta(id_Venta)
);

-- Volver a activar la verificación de llaves foráneas
SET FOREIGN_KEY_CHECKS = 1