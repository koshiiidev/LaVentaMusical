
-- LaVentaMusical
-- Jorge Isaac López Valverde
-- ==========================================

CREATE DATABASE LaVentaMusical;
GO

USE LaVentaMusical;
GO

-- TABLA: Perfil
-- ==========================================
CREATE TABLE Perfil (
    id_perfil INT IDENTITY(1,1) PRIMARY KEY,
    nombre_perfil VARCHAR(30) NOT NULL
);
GO

INSERT INTO Perfil (nombre_perfil) VALUES ('Administrador'), ('Usuario');
GO


-- TABLA: TipoTarjeta
-- ==========================================
CREATE TABLE TipoTarjeta (
    id_tipo_tarjeta INT IDENTITY(1,1) PRIMARY KEY,
    nombre_tarjeta VARCHAR(30) NOT NULL
);
GO

INSERT INTO TipoTarjeta (nombre_tarjeta) VALUES ('VISA'), ('MASTERCARD'), ('AMERICAN EXPRESS');
GO


-- TABLA: Usuario
-- ==========================================
CREATE TABLE Usuario (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    numero_identificacion VARCHAR(20) UNIQUE NOT NULL,
    nombre_completo VARCHAR(100) NOT NULL,
    genero VARCHAR(10) CHECK (genero IN ('Masculino', 'Femenino')) NOT NULL,
    correo VARCHAR(100) NOT NULL,
    id_tipo_tarjeta INT NOT NULL,
    numero_tarjeta VARCHAR(19) NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    id_perfil INT NOT NULL,
    CONSTRAINT FK_Usuario_TipoTarjeta FOREIGN KEY (id_tipo_tarjeta) REFERENCES TipoTarjeta(id_tipo_tarjeta),
    CONSTRAINT FK_Usuario_Perfil FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil)
);
GO

-- TABLA: GeneroMusical
-- ==========================================
CREATE TABLE GeneroMusical (
    id_genero INT IDENTITY(1,1) PRIMARY KEY,
    codigo_genero VARCHAR(10) UNIQUE NOT NULL,
    descripcion VARCHAR(100) NOT NULL
);
GO

-- TABLA: Cancion
-- ==========================================
CREATE TABLE Cancion (
    id_cancion INT IDENTITY(1,1) PRIMARY KEY,
    codigo_cancion VARCHAR(10) UNIQUE NOT NULL,
    id_genero INT NOT NULL,
    nombre_cancion VARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) CHECK (precio >= 0) NOT NULL,
    CONSTRAINT FK_Cancion_Genero FOREIGN KEY (id_genero) REFERENCES GeneroMusical(id_genero)
);
GO

-- TABLA: Venta
-- ==========================================
CREATE TABLE Venta (
    id_venta INT IDENTITY(1,1) PRIMARY KEY,
    numero_factura VARCHAR(20) UNIQUE NOT NULL,
    fecha_compra DATETIME DEFAULT GETDATE(),
    subtotal DECIMAL(10,2) NOT NULL CHECK (subtotal >= 0),
    iva DECIMAL(10,2) NOT NULL CHECK (iva >= 0),
    total DECIMAL(10,2) NOT NULL CHECK (total >= 0),
    id_usuario INT NOT NULL,
    CONSTRAINT FK_Venta_Usuario FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);
GO

-- TABLA: DetalleVenta
-- ==========================================
CREATE TABLE DetalleVenta (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_venta INT NOT NULL,
    id_cancion INT NOT NULL,
    cantidad INT DEFAULT 1 CHECK (cantidad > 0),
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVenta_Venta FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    CONSTRAINT FK_DetalleVenta_Cancion FOREIGN KEY (id_cancion) REFERENCES Cancion(id_cancion)
);
GO

-- VISTAS: 
-- DetalleFactura
-- ==========================================
CREATE VIEW v_DetalleFactura AS
SELECT 
    v.numero_factura,
    u.nombre_completo AS Cliente,
    c.nombre_cancion,
    d.cantidad,
    d.precio_unitario,
    d.subtotal,
    v.subtotal AS SubtotalFactura,
    v.iva,
    v.total,
    v.fecha_compra
FROM Venta v
INNER JOIN Usuario u ON v.id_usuario = u.id_usuario
INNER JOIN DetalleVenta d ON v.id_venta = d.id_venta
INNER JOIN Cancion c ON d.id_cancion = c.id_cancion;
GO


USE LaVentaMusical;
GO

ALTER TABLE Cancion
ADD artista VARCHAR(100) NOT NULL CONSTRAINT DF_Cancion_Artista DEFAULT('Desconocido');
GO

