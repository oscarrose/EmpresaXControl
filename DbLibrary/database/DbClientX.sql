CREATE DATABASE EmpresaX
GO

USE EmpresaX
Go

CREATE TABLE Clients(
client_id INT NOT NULL IDENTITY(1,1) CONSTRAINT PkCliente_Id PRIMARY KEY(client_id),
client_name VARCHAR(25) NOT NULL,
last_name VARCHAR(30) NOT NULL,
identification VARCHAR(20)NOT NULL,
email VARCHAR(255) NULL,
phone_number VARCHAR(25) NULL,
status_client BIT DEFAULT 1,
fecha_creacion DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE client_addresses(
addresses_id INT NOT NULL IDENTITY(1,1) CONSTRAINT PkAddresses_clients PRIMARY KEY(addresses_id),
client_id INT NOT NULL CONSTRAINT FKClient_id_client_addresses FOREIGN KEY REFERENCES clients(client_id),
country VARCHAR(100) NOT NULL,
state_name VARCHAR(30) NULL,
city VARCHAR(30) NOT NULL,
street VARCHAR(255) NOT NULL,
postal_code VARCHAR(20) NULL,
House_number INT NULL
)
GO