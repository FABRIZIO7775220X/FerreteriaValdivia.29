DROP DATABASE IF EXISTS FerreteriaValdiviaDB;
CREATE DATABASE FerreteriaValdiviaDB;
USE FerreteriaValdiviaDB;

-- 1. Categoria
CREATE TABLE Categoria (
    id_Categoria INT PRIMARY KEY,
    Nombre_categoria VARCHAR(100)
);

-- 2. Proveedor
CREATE TABLE Proveedor (
    id_Proveedor INT PRIMARY KEY,
    Nombre VARCHAR(100),
    Telefono VARCHAR(20),
    Ubicacion VARCHAR(200)
);

-- 3. Cliente
CREATE TABLE Cliente (
    id_Cliente INT PRIMARY KEY,
    Nombre VARCHAR(80),
    Telefono VARCHAR(20),
    Direccion VARCHAR(200)
);

-- 4. Empleado
CREATE TABLE Empleado (
    id_Empleado INT PRIMARY KEY,
    Nombre VARCHAR(80),
    Cargo VARCHAR(50),
    Telefono VARCHAR(20)
);

-- 5. Producto (Adaptado para que coincida con tu controlador actual)
CREATE TABLE Productos (
    CodigoProducto VARCHAR(50) NOT NULL PRIMARY KEY,
    Nombre_Producto VARCHAR(100) NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    StockMinimo INT NOT NULL DEFAULT 0,
    StockActual INT NOT NULL DEFAULT 0,
    CantidadVendida INT NOT NULL DEFAULT 0,
    id_Categoria INT,
    id_Proveedor INT,
    FOREIGN KEY (id_Categoria) REFERENCES Categoria(id_Categoria),
    FOREIGN KEY (id_Proveedor) REFERENCES Proveedor(id_Proveedor)
);

-- 6. Venta (Depende de Cliente y Empleado)
CREATE TABLE Venta (
    id_Venta INT PRIMARY KEY,
    Fecha DATE,
    Hora TIME,
    Total DECIMAL(10,2),
    id_Cliente INT,
    id_Empleado INT,
    FOREIGN KEY (id_Cliente) REFERENCES Cliente(id_Cliente),
    FOREIGN KEY (id_Empleado) REFERENCES Empleado(id_Empleado)
);

-- 7. Detalle_Venta (Depende de Venta y Productos)
CREATE TABLE Detalle_Venta (
    id_Detalle INT PRIMARY KEY,
    Cantidad INT,
    Precio_Unitario DECIMAL(10,2),
    Subtotal DECIMAL(10,2),
    id_Venta INT,
    CodigoProducto VARCHAR(50),
    FOREIGN KEY (id_Venta) REFERENCES Venta(id_Venta),
    FOREIGN KEY (CodigoProducto) REFERENCES Productos(CodigoProducto)
);

-- 8. Pago (Depende de Venta)
CREATE TABLE Pago (
    id_Pago INT PRIMARY KEY,
    Monto DECIMAL(10,2),
    Metodo_Pago VARCHAR(50),
    Fecha DATE,
    Hora TIME,
    id_Venta INT,
    FOREIGN KEY (id_Venta) REFERENCES Venta(id_Venta)
);