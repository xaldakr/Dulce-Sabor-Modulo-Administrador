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