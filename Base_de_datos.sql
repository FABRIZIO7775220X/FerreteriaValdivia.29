CREATE DATABASE IF NOT EXISTS `FerreteriaValdivia`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE `FerreteriaValdivia`;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

START TRANSACTION;

-- 1. Catálogos base
CREATE TABLE IF NOT EXISTS `Rol` (
    `id_Rol` int NOT NULL AUTO_INCREMENT,
    `Nombre_Rol` varchar(80) NOT NULL,
    PRIMARY KEY (`id_Rol`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Categoria` (
    `id_Categoria` int NOT NULL AUTO_INCREMENT,
    `Nombre_categoria` varchar(100) NOT NULL,
    PRIMARY KEY (`id_Categoria`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Proveedor` (
    `id_Proveedor` int NOT NULL AUTO_INCREMENT,
    `Nombre` varchar(100) NOT NULL,
    `Telefono` varchar(20) NOT NULL,
    `Ubicacion` varchar(200) NOT NULL,
    PRIMARY KEY (`id_Proveedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Cliente` (
    `id_Cliente` int NOT NULL AUTO_INCREMENT,
    `Nombre` varchar(80) NOT NULL,
    `Telefono` varchar(20) NOT NULL,
    `Direccion` varchar(200) NOT NULL,
    PRIMARY KEY (`id_Cliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 2. Entidades dependientes de catálogos
CREATE TABLE IF NOT EXISTS `Empleado` (
    `id_Empleado` int NOT NULL AUTO_INCREMENT,
    `Nombre` varchar(80) NOT NULL,
    `Cargo` varchar(50) NOT NULL,
    `Telefono` varchar(20) NOT NULL,
    `id_Rol` int NULL,
    PRIMARY KEY (`id_Empleado`),
    CONSTRAINT `FK_Empleado_Rol_id_Rol`
        FOREIGN KEY (`id_Rol`) REFERENCES `Rol` (`id_Rol`)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Caja` (
    `id_Caja` int NOT NULL AUTO_INCREMENT,
    `Fecha_Apertura` datetime(6) NOT NULL,
    `Monto_Inicial` decimal(18,2) NOT NULL,
    `Monto_Final` decimal(18,2) NULL,
    `id_Empleado` int NOT NULL,
    PRIMARY KEY (`id_Caja`),
    CONSTRAINT `FK_Caja_Empleado_id_Empleado`
        FOREIGN KEY (`id_Empleado`) REFERENCES `Empleado` (`id_Empleado`)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Productos` (
    `CodigoProducto` varchar(50) NOT NULL,
    `Nombre_Producto` varchar(100) NOT NULL,
    `PrecioVenta` decimal(18,2) NOT NULL,
    `StockMinimo` int NOT NULL,
    `StockActual` int NOT NULL,
    `CantidadVendida` int NOT NULL,
    `CantidadVenta` int NOT NULL,
    `id_Categoria` int NULL,
    `id_Proveedor` int NULL,
    PRIMARY KEY (`CodigoProducto`),
    CONSTRAINT `FK_Productos_Categoria_id_Categoria`
        FOREIGN KEY (`id_Categoria`) REFERENCES `Categoria` (`id_Categoria`)
        ON DELETE SET NULL,
    CONSTRAINT `FK_Productos_Proveedor_id_Proveedor`
        FOREIGN KEY (`id_Proveedor`) REFERENCES `Proveedor` (`id_Proveedor`)
        ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 3. Flujo transaccional
CREATE TABLE IF NOT EXISTS `Venta` (
    `id_Venta` int NOT NULL AUTO_INCREMENT,
    `Fecha` datetime(6) NOT NULL,
    `Hora` time(6) NOT NULL,
    `Total` decimal(18,2) NOT NULL,
    `id_Cliente` int NOT NULL,
    `id_Empleado` int NOT NULL,
    `id_Caja` int NOT NULL,
    PRIMARY KEY (`id_Venta`),
    CONSTRAINT `FK_Venta_Caja_id_Caja`
        FOREIGN KEY (`id_Caja`) REFERENCES `Caja` (`id_Caja`)
        ON DELETE RESTRICT,
    CONSTRAINT `FK_Venta_Cliente_id_Cliente`
        FOREIGN KEY (`id_Cliente`) REFERENCES `Cliente` (`id_Cliente`)
        ON DELETE RESTRICT,
    CONSTRAINT `FK_Venta_Empleado_id_Empleado`
        FOREIGN KEY (`id_Empleado`) REFERENCES `Empleado` (`id_Empleado`)
        ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `DetalleVenta` (
    `id_DetalleVenta` int NOT NULL AUTO_INCREMENT,
    `id_Venta` int NOT NULL,
    `id_Producto` varchar(50) NOT NULL,
    `Cantidad` int NOT NULL,
    `Precio_Unitario` decimal(18,2) NOT NULL,
    `Subtotal` decimal(18,2) NOT NULL,
    PRIMARY KEY (`id_DetalleVenta`),
    CONSTRAINT `FK_DetalleVenta_Productos_id_Producto`
        FOREIGN KEY (`id_Producto`) REFERENCES `Productos` (`CodigoProducto`)
        ON DELETE RESTRICT,
    CONSTRAINT `FK_DetalleVenta_Venta_id_Venta`
        FOREIGN KEY (`id_Venta`) REFERENCES `Venta` (`id_Venta`)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `Pago` (
    `id_Pago` int NOT NULL AUTO_INCREMENT,
    `Monto` decimal(18,2) NOT NULL,
    `MetodoPago` varchar(50) NOT NULL,
    `Fecha` date NOT NULL,
    `Hora` time(6) NOT NULL,
    `id_Venta` int NOT NULL,
    PRIMARY KEY (`id_Pago`),
    CONSTRAINT `FK_Pago_Venta_id_Venta`
        FOREIGN KEY (`id_Venta`) REFERENCES `Venta` (`id_Venta`)
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT IGNORE INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20261123173928_InitialMariaDb', '9.0.0');

COMMIT;