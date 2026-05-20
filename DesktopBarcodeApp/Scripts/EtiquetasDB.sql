CREATE DATABASE EtiquetasDB;
GO

USE EtiquetasDB;
GO

CREATE TABLE Etiquetas
(
    Id INT PRIMARY KEY IDENTITY,
    Empresa VARCHAR(100),
    Direccion VARCHAR(100),
    Ciudad VARCHAR(100),
    Producto VARCHAR(100),
    Composicion VARCHAR(100),
    Origen VARCHAR(100),
    Codigo VARCHAR(100),
    CodigoInterno VARCHAR(100),
    Metros VARCHAR(100),    
    FechaCreacion DATETIME DEFAULT GETDATE()
);