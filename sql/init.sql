CREATE DATABASE DesafioThomasGreg
GO
USE DesafioThomasGreg
GO

CREATE TABLE Usuario ( 
Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
Nome NVARCHAR(100) NOT NULL,
Email NVARCHAR(100) NOT NULL UNIQUE,
SenhaHash NVARCHAR(255) NOT NULL
);

GO

CREATE TABLE Cliente (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Logotipo VARBINARY(MAX) NULL
);

GO
 
CREATE TABLE Logradouro (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ClienteId UNIQUEIDENTIFIER NOT NULL,
    Endereco NVARCHAR(255) NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(Id) ON DELETE CASCADE
);

GO 

CREATE PROCEDURE sp_CriarCliente 
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100),
    @Logotipo VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Cliente (Nome, Email, Logotipo)
    VALUES (@Nome, @Email, @Logotipo);
END

GO 

CREATE PROCEDURE sp_AtualizarCliente
    @Id UNIQUEIDENTIFIER,
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100),
    @Logotipo VARBINARY(MAX)
AS
BEGIN
    UPDATE Cliente
    SET Nome = @Nome,
        Email = @Email,
        Logotipo = @Logotipo
    WHERE Id = @Id;
END 

GO 

CREATE PROCEDURE sp_RemoverCliente
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Cliente
    WHERE Id = @Id;
END

GO 

CREATE PROCEDURE sp_AdicionarLogradouro 
    @ClienteId UNIQUEIDENTIFIER,
    @Endereco NVARCHAR(255)
AS
BEGIN
    INSERT INTO Logradouro (ClienteId, Endereco)
    VALUES (@ClienteId, @Endereco);
END

GO 

CREATE PROCEDURE sp_RemoverLogradouro
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM Logradouro
    WHERE Id = @Id;
END

GO 

CREATE PROCEDURE sp_CriarUsuario 
    @Nome NVARCHAR(100),
    @Email NVARCHAR(100),
    @SenhaHash NVARCHAR(255)
AS
BEGIN
    INSERT INTO Usuario (Nome, Email, SenhaHash)
    VALUES (@Nome, @Email, @SenhaHash);
END

GO 

CREATE PROCEDURE sp_AtualizarLogradouro
    @Id UNIQUEIDENTIFIER,
    @ClienteId UNIQUEIDENTIFIER,
    @Endereco NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Logradouro
    SET Endereco = @Endereco, ClienteId = @ClienteId
    WHERE Id = @Id;
END
 
GO 
