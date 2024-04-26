CREATE TABLE Cuentas (
    id_cuenta INT IDENTITY(1,1) PRIMARY KEY,
    id_mesa INT NOT NULL,
    fecha_apertura DATETIME,
    estado VARCHAR(10) CHECK (estado IN ('ABIERTO', 'CERRADO'))
);

CREATE TABLE DetalleCuentas (
    id_detalle_cuenta INT IDENTITY(1,1) PRIMARY KEY,
    id_cuenta INT NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    descuento DECIMAL(3,2) DEFAULT 0 CHECK (descuento >= 0 AND descuento <= 1),
    estado VARCHAR(20) CHECK (estado IN ('SOLICITADO', 'EN PROCESO', 'FINALIZADO', 'ENTREGADO', 'CANCELADO')),
);

CREATE TABLE Empleado(
	id int IDENTITY(1,1) PRIMARY KEY,
	nombre varchar(50) NULL,
);

CREATE TABLE Mesa(
	id int IDENTITY(1,1) PRIMARY KEY,
	numero_mesa int NULL,
	estado_mesa bit NULL,
);

CREATE TABLE DetallesPedidos(
    id_detalle_pedido int IDENTITY(1,1) PRIMARY KEY,
    id_venta int,
    fecha_creacion date,
    precio float,
    FOREIGN KEY (id_venta) REFERENCES Ventas(id)
);

CREATE TABLE Ventas(
    id int IDENTITY(1,1) PRIMARY KEY,
    fecha date,
    estado char,
);
CREATE TABLE DetalleVentas(
    id_detalle_pedido int IDENTITY(1,1) PRIMARY KEY,
    id_venta int,
    fecha_creacion date,
    precio float,
    FOREIGN KEY (id_venta) REFERENCES Ventas(id)
);