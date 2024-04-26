CREATE TABLE Cuentas (
    id_cuenta INT IDENTITY(1,1) PRIMARY KEY,
    id_mesa INT NOT NULL,
    nombre VARCHAR(255) NOT NULL,
    apellido VARCHAR(255) NOT NULL,
    fecha_apertura DATETIME,
    estado VARCHAR(10) CHECK (estado IN ('ABIERTO', 'CERRADO'))
);

CREATE TABLE DetalleCuentas (
    id_detalle_cuenta INT IDENTITY(1,1) PRIMARY KEY,
    id_cuenta INT NOT NULL,
    id_plato INT NULL,
    id_combo INT NULL,
    id_promocion INT NULL,
    precio DECIMAL(10,2) NOT NULL,
    descuento DECIMAL(3,2) DEFAULT 0 CHECK (descuento >= 0 AND descuento <= 1),
    comentario TEXT NULL,
    estado VARCHAR(20) CHECK (estado IN ('SOLICITADO', 'EN PROCESO', 'FINALIZADO', 'ENTREGADO', 'CANCELADO')),
    FOREIGN KEY (id_cuenta) REFERENCES Cuentas(id_cuenta),
    FOREIGN KEY (id_plato) REFERENCES platos(plato_id),
    FOREIGN KEY (id_combo) REFERENCES combos(combo_id),
    FOREIGN KEY (id_promocion) REFERENCES promociones(promocion_id)
);

CREATE TABLE [dbo].[Empleado](
	id int IDENTITY(1,1) NOT NULL,
	nombre varchar(50) NULL,
	dui varchar(10) NULL,
	fecha_nacimiento date NULL,
	telefono varchar(20) NULL,
	direccion varchar(40) NULL,
	telefono_referencia varchar(20) NULL,
	cargo_id int NULL,
	disponible bit NULL,
	salario decimal(18, 2) NULL,
	fecha_contratacion date NULL,
);