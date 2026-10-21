CREATE DATABASE db_academia
USE db_academia
GO

CREATE TABLE [Sueldos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Valor] INT NOT NULL
);

INSERT INTO [Sueldos] ([Valor])
VALUES (1500000);

CREATE TABLE [Personas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[FechaNacimiento] SMALLDATETIME NOT NULL,
	[Nacionalidad] NVARCHAR(50) NOT NULL,
	[Sueldo] INT NOT NULL UNIQUE REFERENCES [Sueldos]([Id])
);

INSERT INTO [Personas] ([Nombre], [FechaNacimiento], [Nacionalidad], [Sueldo])
VALUES ('Juan Perez', '1998-05-10', 'Colombiana', 1);

CREATE TABLE [Equipos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Siglas] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaCrea] SMALLDATETIME NOT NULL
);

INSERT INTO [Equipos] ([Nombre], [Siglas], [Descripcion], [FechaCrea])
VALUES ('Los Titanes', 'LT', 'Equipo profesional de esports', GETDATE());

CREATE TABLE [Jugadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id])
);

INSERT INTO [Jugadores] ([Activo], [Persona], [Equipo])
VALUES (1, 1, 1);

CREATE TABLE [Entrenadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id])
);

INSERT INTO [Entrenadores] ([Activo], [Persona], [Equipo])
VALUES (1, 1, 1);

CREATE TABLE [Analistas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id])
);

INSERT INTO [Analistas] ([Activo], [Persona], [Equipo])
VALUES (1, 1, 1);

CREATE TABLE [Psicologos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id])
);

INSERT INTO [Psicologos] ([Activo], [Persona], [Equipo])
VALUES (1, 1, 1);

CREATE TABLE [Fisioterapeutas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id])
);

INSERT INTO [Fisioterapeutas] ([Activo], [Persona], [Equipo])
VALUES (1, 1, 1);

CREATE TABLE [Organizadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id])
);

INSERT INTO [Organizadores] ([Activo], [Persona])
VALUES (1, 1);

CREATE TABLE [Juegos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Genero] NVARCHAR(50) NOT NULL,
	[CantJug] INT NOT NULL,
	[Pago] BIT NOT NULL
);

INSERT INTO [Juegos] ([Nombre], [Genero], [CantJug], [Pago])
VALUES ('League of Legends', 'MOBA', 5, 0);

CREATE TABLE [Equipos_Juegos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Juego] INT NOT NULL REFERENCES [Juegos]([Id])
);

INSERT INTO [Equipos_Juegos] ([Equipo], [Juego])
VALUES (1, 1);

CREATE TABLE [Tipo] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripccion] NVARCHAR(200) NOT NULL
);

INSERT INTO [Tipo] ([Nombre], [Descripccion])
VALUES ('Patrocinador', 'Empresa que apoya económicamente a los equipos');

CREATE TABLE [Empresas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Pais] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaCrea] SMALLDATETIME NOT NULL,
	[Correo] NVARCHAR(50) NOT NULL,
	[Tipo] INT NOT NULL REFERENCES [Tipo]([Id])
);

INSERT INTO [Empresas] ([Nombre], [Pais], [Descripcion], [FechaCrea], [Correo], [Tipo])
VALUES ('TechCorp', 'Colombia', 'Empresa de tecnología', GETDATE(), 'contacto@techcorp.com', 1);

CREATE TABLE [Equipos_Empresas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Empresa] INT NOT NULL REFERENCES [Empresas]([Id])
);

INSERT INTO [Equipos_Empresas] ([Equipo], [Empresa])
VALUES (1, 1);

CREATE TABLE [Academias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Correo] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaCrea] SMALLDATETIME NOT NULL,
	[Nacional] BIT NOT NULL
);

INSERT INTO [Academias] ([Nombre], [Correo], [Descripcion], [FechaCrea], [Nacional])
VALUES ('Academia Gamer Pro', 'info@gamerpro.com', 'Academia de formación en esports', GETDATE(), 1);

CREATE TABLE [Equipos_Academias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Academia] INT NOT NULL REFERENCES [Academias]([Id])
);

INSERT INTO [Equipos_Academias] ([Equipo], [Academia])
VALUES (1, 1);

CREATE TABLE [Empresas_Academias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Empresa] INT NOT NULL REFERENCES [Empresas]([Id]),
	[Academia] INT NOT NULL REFERENCES [Academias]([Id])
);

INSERT INTO [Empresas_Academias] ([Empresa], [Academia])
VALUES (1, 1);

CREATE TABLE [Eventos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaIni] SMALLDATETIME NOT NULL,
	[FechaFin] SMALLDATETIME NOT NULL,
	[Activo] BIT NOT NULL,
	[Modalidad] NVARCHAR(50) NOT NULL,
	[Ciudad] NVARCHAR(50) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL
);

INSERT INTO [Eventos] ([Nombre], [Descripcion], [FechaIni], [FechaFin], [Activo], [Modalidad], [Ciudad], [Direccion])
VALUES ('Torneo Nacional 2026', 'Torneo clasificatorio nacional', GETDATE(), GETDATE(), 1, 'Presencial', 'Medellín', 'Calle 10 # 20-30');

CREATE TABLE [Empresas_Academias_Eventos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Empresa] INT NOT NULL REFERENCES [Empresas]([Id]),
	[Academia] INT NOT NULL REFERENCES [Academias]([Id]),
	[Evento] INT NOT NULL REFERENCES [Eventos]([Id])
);

INSERT INTO [Empresas_Academias_Eventos] ([Empresa], [Academia], [Evento])
VALUES (1, 1, 1);

CREATE TABLE [Eventos_Organizadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Evento] INT NOT NULL REFERENCES [Eventos]([Id]),
	[Organizador] INT NOT NULL REFERENCES [Organizadores]([Id])
);

INSERT INTO [Eventos_Organizadores] ([Evento], [Organizador])
VALUES (1, 1);

CREATE TABLE [Entretenimientos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[Duracion] INT NOT NULL,
	[Evento] INT NOT NULL REFERENCES [Eventos]([Id])
);

INSERT INTO [Entretenimientos] ([Nombre], [Descripcion], [Duracion], [Evento])
VALUES ('Show de drones', 'Espectáculo de drones antes del torneo', 30, 1);

-- errorcito para arreglar que muchas personas tengas el mismo sueldo
DECLARE @nombre NVARCHAR(200);

SELECT @nombre = name
FROM sys.key_constraints
WHERE parent_object_id = OBJECT_ID('dbo.Personas') AND type = 'UQ';

IF @nombre IS NOT NULL
    EXEC('ALTER TABLE [dbo].[Personas] DROP CONSTRAINT [' + @nombre + ']');
ELSE
    PRINT 'No hay ninguna restricción UNIQUE en Personas';