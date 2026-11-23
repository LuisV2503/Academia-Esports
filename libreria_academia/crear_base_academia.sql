-- Base de datos Academia desde cero (BORRA la base si existe y todos sus datos).
-- Cierra el back y el front antes de ejecutarlo.

USE master;
GO

IF DB_ID('db_academia') IS NOT NULL
BEGIN
    ALTER DATABASE db_academia SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE db_academia;
END
GO

CREATE DATABASE db_academia;
GO

USE db_academia;
GO

CREATE TABLE [Sueldos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Valor] INT NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Sueldos] ([Valor], [Imagen])
VALUES (1500000, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AIADAQIblqdt7xggmyoM7A036vN8/TpgnoptwxuqM1RmrM1ZnrM5YnbE6Y3XG6ozVGasz961ek9sCPxZldL0AAAAASUVORK5CYII=');

INSERT INTO [Sueldos] ([Valor], [Imagen])
VALUES (2500000, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaT6ZjCuSaYKA/uDDTp837/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6Ae+bATUL7XKYAAAAAElFTkSuQmCC');

CREATE TABLE [Personas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[FechaNacimiento] SMALLDATETIME NOT NULL,
	[Nacionalidad] NVARCHAR(50) NOT NULL,
	[Sueldo] INT NOT NULL REFERENCES [Sueldos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Personas] ([Nombre], [FechaNacimiento], [Nacionalidad], [Sueldo], [Imagen])
VALUES (N'Juan Perez', '1998-05-10', N'Colombiana', 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIacSZjMKSaYKA/uDDTp8/7fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6ATFXAUaIxf01AAAAAElFTkSuQmCC');

INSERT INTO [Personas] ([Nombre], [FechaNacimiento], [Nacionalidad], [Sueldo], [Imagen])
VALUES (N'Maria Gomez', '2000-08-21', N'Venezolana', 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIan6ZrK+SKYKA/uDDTp83/vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6Aa/BAbl9zXzdAAAAAElFTkSuQmCC');

CREATE TABLE [Equipos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Siglas] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaCrea] SMALLDATETIME NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Equipos] ([Nombre], [Siglas], [Descripcion], [FechaCrea], [Imagen])
VALUES (N'Los Titanes', N'LT', N'Equipo profesional de esports', GETDATE(), 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AIADAQIb/ZdJmiWCiPLgz0KTP/37jhHkiuglnrM5YnbE6Y3XG6ozVGaszVmeszlidsTpz3+oFSNMCO3Z7r6gAAAAASUVORK5CYII=');

INSERT INTO [Equipos] ([Nombre], [Siglas], [Descripcion], [FechaCrea], [Imagen])
VALUES (N'Aguilas Rojas', N'AR', N'Equipo juvenil de esports', GETDATE(), 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbHyZnO2SGYKA/uDDTp8/3vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AWkkAfkMpgjZAAAAAElFTkSuQmCC');

CREATE TABLE [Jugadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Jugadores] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbESZukKSOYKA/uDDTp87/fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6ATB6AfaBLsxOAAAAAElFTkSuQmCC');

INSERT INTO [Jugadores] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AIADAQIb8SdpzyggmyoM7A036fO8/TpgnoptwxuqM1RmrM1ZnrM5YnbE6Y3XG6ozVGasz961eRusCVuY+H4oAAAAASUVORK5CYII=');

CREATE TABLE [Entrenadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Entrenadores] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIav6Zj/x3wQTJQHdwaa9Pm/d5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDeSAHJ10ZlCgAAAABJRU5ErkJggg==');

INSERT INTO [Entrenadores] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIa2SZvIySGYKA/uDDTp8/3vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6Ab8XAfDjtvCvAAAAAElFTkSuQmCC');

CREATE TABLE [Analistas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Analistas] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIa+yZyQSSOYKA/uDDTp8/3vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6Ae2eAgAiEb2mAAAAAElFTkSuQmCC');

INSERT INTO [Analistas] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaiCZyUiSWYKA/uDDTp87/fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AT+NAZhsFcvhAAAAAElFTkSuQmCC');

CREATE TABLE [Psicologos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Psicologos] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AIADAQIb6PSdo4ggmyoM7A036vP83TpgnoptwxuqM1RmrM1ZnrM5YnbE6Y3XG6ozVGasz961elcMCJLobPv8AAAAASUVORK5CYII=');

INSERT INTO [Psicologos] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaf6ZyS+SSYKA/uDDTp83/vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AYE6AaklS63YAAAAAElFTkSuQmCC');

CREATE TABLE [Fisioterapeutas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Fisioterapeutas] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIaSCZh/BRNGMFEe3Blo0ud7/3HCPBHdhDNWZ6zOWJ2xOmN1xuqM1RmrM1ZnrM5Ynblv9QIcSQFgrObaGgAAAABJRU5ErkJggg==');

INSERT INTO [Fisioterapeutas] ([Activo], [Persona], [Equipo], [Imagen])
VALUES (1, 2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaUyZjcKSWYKA/uDDTp873/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AcgvAU6EIz4FAAAAAElFTkSuQmCC');

CREATE TABLE [Organizadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Activo] BIT NOT NULL,
	[Persona] INT NOT NULL REFERENCES [Personas]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Organizadores] ([Activo], [Persona], [Imagen])
VALUES (1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbNSZqCuSWYKA/uDDTp8/7fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AU7jAc/jgHHjAAAAAElFTkSuQmCC');

INSERT INTO [Organizadores] ([Activo], [Persona], [Imagen])
VALUES (1, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIaR+X9Mx2wRTJQHdwaa9Hm/f5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDIcgHjGBgRgAAAAABJRU5ErkJggg==');

CREATE TABLE [Juegos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Genero] NVARCHAR(50) NOT NULL,
	[CantJug] INT NOT NULL,
	[Pago] BIT NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Juegos] ([Nombre], [Genero], [CantJug], [Pago], [Imagen])
VALUES (N'League of Legends', N'MOBA', 5, 0, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIa2CZnMySOYKA/uDDTp873/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AbkcAawac+0BAAAAAElFTkSuQmCC');

INSERT INTO [Juegos] ([Nombre], [Genero], [CantJug], [Pago], [Imagen])
VALUES (N'Valorant', N'Shooter', 5, 0, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIalqZjcOSSYKA/uDDTp87/fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AUF1AX0RYqqFAAAAAElFTkSuQmCC');

CREATE TABLE [Equipos_Juegos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Juego] INT NOT NULL REFERENCES [Juegos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Equipos_Juegos] ([Equipo], [Juego], [Imagen])
VALUES (1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIadKZr+GSGYKA/uDDTp837/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AYitAbdLtHdRAAAAAElFTkSuQmCC');

INSERT INTO [Equipos_Juegos] ([Equipo], [Juego], [Imagen])
VALUES (2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIb+CZuIiSGYKA/uDDTp83/vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AWEsAoDepaKLAAAAAElFTkSuQmCC');

CREATE TABLE [Tipo] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripccion] NVARCHAR(200) NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Tipo] ([Nombre], [Descripccion], [Imagen])
VALUES (N'Patrocinador', N'Empresa que apoya económicamente a los equipos', 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIZ/AROwx2QSTJQHdwaa9Pm/d5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDESgI0veDj3wAAAABJRU5ErkJggg==');

INSERT INTO [Tipo] ([Nombre], [Descripccion], [Imagen])
VALUES (N'Aliado', N'Empresa aliada de la academia', 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AIADAQIb5Kdl7FgkmyoM7A036/N87TpgnoptwxuqM1RmrM1ZnrM5YnbE6Y3XG6ozVGasz961eGkwCK8kW+5gAAAAASUVORK5CYII=');

CREATE TABLE [Empresas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Pais] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaCrea] SMALLDATETIME NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL,
	[Tipo] INT NOT NULL REFERENCES [Tipo]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Empresas] ([Nombre], [Pais], [Descripcion], [FechaCrea], [Correo], [Tipo], [Imagen])
VALUES (N'TechCorp', N'Colombia', N'Empresa de tecnología', GETDATE(), N'contacto@techcorp.com', 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIaVKdl7/v0QTJQHdwaa9Pm/d5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDzawD/UE5x+QAAAABJRU5ErkJggg==');

INSERT INTO [Empresas] ([Nombre], [Pais], [Descripcion], [FechaCrea], [Correo], [Tipo], [Imagen])
VALUES (N'GameStore', N'Venezuela', N'Tienda de videojuegos', GETDATE(), N'info@gamestore.com', 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQElEQVR4Xu3QQQ3AMAzAwK5Awh/mNBLOY3cELPmZmbPhbkQ/whmrM1ZnrM5YnbE6Y3XG6ozVGaszVmeszvxv9QusiwCqc5kk2QAAAABJRU5ErkJggg==');

CREATE TABLE [Equipos_Empresas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Empresa] INT NOT NULL REFERENCES [Empresas]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Equipos_Empresas] ([Equipo], [Empresa], [Imagen])
VALUES (1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbASZikaSaYKA/uDDTp837/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6ATaKAYr0Q+aXAAAAAElFTkSuQmCC');

INSERT INTO [Equipos_Empresas] ([Equipo], [Empresa], [Imagen])
VALUES (2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbzSZmOmSOYKA/uDDTp8/7fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AT/QAi1ffee2AAAAAElFTkSuQmCC');

CREATE TABLE [Academias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaCrea] SMALLDATETIME NOT NULL,
	[Nacional] BIT NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Academias] ([Nombre], [Correo], [Descripcion], [FechaCrea], [Nacional], [Imagen])
VALUES (N'Academia Gamer Pro', N'info@gamerpro.com', N'Academia de formación en esports', GETDATE(), 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaf6ZyS+SSYKA/uDDTp83/vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AYE6AaklS63YAAAAAElFTkSuQmCC');

INSERT INTO [Academias] ([Nombre], [Correo], [Descripcion], [FechaCrea], [Nacional], [Imagen])
VALUES (N'Academia Latam', N'contacto@latam.com', N'Academia regional de esports', GETDATE(), 0, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbPSZuWWSOYKA/uDDTp8/3vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AZerAgnbv5WnAAAAAElFTkSuQmCC');

CREATE TABLE [Equipos_Academias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Equipo] INT NOT NULL REFERENCES [Equipos]([Id]),
	[Academia] INT NOT NULL REFERENCES [Academias]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Equipos_Academias] ([Equipo], [Academia], [Imagen])
VALUES (1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbSiZmASSaYKA/uDDTp83/vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AX9SAcSw19ZYAAAAAElFTkSuQmCC');

INSERT INTO [Equipos_Academias] ([Equipo], [Academia], [Imagen])
VALUES (2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaSiZyoySSYKA/uDDTp837/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AVwOAYxcmN6yAAAAAElFTkSuQmCC');

CREATE TABLE [Empresas_Academias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Empresa] INT NOT NULL REFERENCES [Empresas]([Id]),
	[Academia] INT NOT NULL REFERENCES [Academias]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Empresas_Academias] ([Empresa], [Academia], [Imagen])
VALUES (1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbJqZi+2SOYKA/uDDTp83/vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AQPbAcsbBwWdAAAAAElFTkSuQmCC');

INSERT INTO [Empresas_Academias] ([Empresa], [Academia], [Imagen])
VALUES (2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbeCZyQCSKYKA/uDDTp837/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AT3oAkggDP6HAAAAAElFTkSuQmCC');

CREATE TABLE [Eventos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[FechaIni] SMALLDATETIME NOT NULL,
	[FechaFin] SMALLDATETIME NOT NULL,
	[Activo] BIT NOT NULL,
	[Modalidad] NVARCHAR(50) NOT NULL,
	[Ciudad] NVARCHAR(50) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Eventos] ([Nombre], [Descripcion], [FechaIni], [FechaFin], [Activo], [Modalidad], [Ciudad], [Direccion], [Imagen])
VALUES (N'Torneo Nacional 2026', N'Torneo clasificatorio nacional', GETDATE(), DATEADD(DAY, 1, GETDATE()), 1, N'Presencial', N'Medellín', N'Calle 10 # 20-30', 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIaRveZfzfwQTJQHdwaa9Pnef5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDGRwFprobUCwAAAABJRU5ErkJggg==');

INSERT INTO [Eventos] ([Nombre], [Descripcion], [FechaIni], [FechaFin], [Activo], [Modalidad], [Ciudad], [Direccion], [Imagen])
VALUES (N'Copa Latam 2026', N'Copa regional en línea', GETDATE(), DATEADD(DAY, 2, GETDATE()), 1, N'Virtual', N'Bogotá', N'Plataforma en línea', 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AMBDAsK5si6lkp5HwPRYTiJTnnrsm7InopzDTaqbVTKuZVjOtZlrNtJppNdNqptVMq5n/rX4Bq/EB79Tx+UgAAAAASUVORK5CYII=');

CREATE TABLE [Empresas_Academias_Eventos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Empresa] INT NOT NULL REFERENCES [Empresas]([Id]),
	[Academia] INT NOT NULL REFERENCES [Academias]([Id]),
	[Evento] INT NOT NULL REFERENCES [Eventos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Empresas_Academias_Eventos] ([Empresa], [Academia], [Evento], [Imagen])
VALUES (1, 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIav6Zj/x3wQTJQHdwaa9Pm/d5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDeSAHJ10ZlCgAAAABJRU5ErkJggg==');

INSERT INTO [Empresas_Academias_Eventos] ([Empresa], [Academia], [Evento], [Imagen])
VALUES (2, 2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIakyZnsKSKYKA/uDDTp837/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6Ad6gAa5dWa/eAAAAAElFTkSuQmCC');

CREATE TABLE [Eventos_Organizadores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Evento] INT NOT NULL REFERENCES [Eventos]([Id]),
	[Organizador] INT NOT NULL REFERENCES [Organizadores]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Eventos_Organizadores] ([Evento], [Organizador], [Imagen])
VALUES (1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AMBDAsK4IC7YAp5HwPRYTiJTnnrsm7InopzDTaqbVTKuZVjOtZlrNtJppNdNqptVMq5n/rX4BhIUCCAXOTzcAAAAASUVORK5CYII=');

INSERT INTO [Eventos_Organizadores] ([Evento], [Organizador], [Imagen])
VALUES (2, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIay+VcwARNCMFEe3Blo0uf733HCPBHdhDNWZ6zOWJ2xOmN1xuqM1RmrM1ZnrM5Ynblv9QKKgAJMH+nM1wAAAABJRU5ErkJggg==');

CREATE TABLE [Entretenimientos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[Duracion] INT NOT NULL,
	[Evento] INT NOT NULL REFERENCES [Eventos]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Entretenimientos] ([Nombre], [Descripcion], [Duracion], [Evento], [Imagen])
VALUES (N'Show de drones', N'Espectáculo de drones antes del torneo', 30, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQElEQVR4Xu3QQQ3AMAzAwK5Awh/mNBLOY3cELPmZmbPhbkQ/whmrM1ZnrM5YnbE6Y3XG6ozVGaszVmeszvxv9QusiwCqc5kk2QAAAABJRU5ErkJggg==');

INSERT INTO [Entretenimientos] ([Nombre], [Descripcion], [Duracion], [Evento], [Imagen])
VALUES (N'Concurso de cosplay', N'Concurso para los asistentes', 45, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIajSZmemSeYKA/uDDTp8/3vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AbXRAU3ylOljAAAAAElFTkSuQmCC');

CREATE TABLE [Roles] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL UNIQUE,
	[Descripcion] NVARCHAR(200) NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Roles] ([Nombre], [Descripcion], [Imagen])
VALUES (N'Administrador', N'Acceso total a la aplicación', 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AIADAQIb5Kdl7FgkmyoM7A036/N87TpgnoptwxuqM1RmrM1ZnrM5YnbE6Y3XG6ozVGasz961eGkwCK8kW+5gAAAAASUVORK5CYII=');

INSERT INTO [Roles] ([Nombre], [Descripcion], [Imagen])
VALUES (N'Usuario', N'Acceso limitado a la aplicación', 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIaraZ6kmSGYKA/uDDTp87/fOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AQkOAg838sRYAAAAAElFTkSuQmCC');

CREATE TABLE [Usuarios] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL UNIQUE,
	[Activo] BIT NOT NULL,
	[Rol] INT NOT NULL REFERENCES [Roles]([Id]),
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Usuarios] ([Nombre], [Correo], [Activo], [Rol], [Imagen])
VALUES (N'Administrador', N'admin@academia.com', 1, 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQUlEQVR4Xu3QQQ3AMBDAsK5si6lkp5HwPRYTiJTnnrsm7InopzDTaqbVTKuZVjOtZlrNtJppNdNqptVMq5n/rX4Bq/EB79Tx+UgAAAAASUVORK5CYII=');

INSERT INTO [Usuarios] ([Nombre], [Correo], [Activo], [Rol], [Imagen])
VALUES (N'Usuario de prueba', N'usuario@academia.com', 1, 2, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIa4yZmc6SWYKA/uDDTp873/OGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6AWURAZqH3VoeAAAAAElFTkSuQmCC');

CREATE TABLE [Logins] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Usuario] INT NOT NULL UNIQUE REFERENCES [Usuarios]([Id]),
	[Contrasena] NVARCHAR(64) NOT NULL,
	[Activo] BIT NOT NULL,
	[Imagen] NVARCHAR(MAX) NULL
);

INSERT INTO [Logins] ([Usuario], [Contrasena], [Activo], [Imagen])
VALUES (1, CONVERT(NVARCHAR(64), HASHBYTES('SHA2_256', 'Admin123'), 2), 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQ0lEQVR4Xu3QQQ3AIADAQIaRveZfzfwQTJQHdwaa9Pnef5wwT0Q34YzVGaszVmeszlidsTpjdcbqjNUZqzNWZ+5bvQDGRwFprobUCwAAAABJRU5ErkJggg==');

INSERT INTO [Logins] ([Usuario], [Contrasena], [Activo], [Imagen])
VALUES (2, CONVERT(NVARCHAR(64), HASHBYTES('SHA2_256', 'Usuario123'), 2), 1, 'iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAIAAAADnC86AAAAQklEQVR4Xu3QQQ3AIADAQIbYSZjU6SGYKA/uDDTp8/3vOGGeiG7CGaszVmeszlidsTpjdcbqjNUZqzNWZ6zO3Ld6ARMxAgJ/2W+yAAAAAElFTkSuQmCC');

-- Auditorías: cada operación queda registrada por la aplicación
CREATE TABLE [Auditorias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Fecha] DATETIME NOT NULL,
	[Usuario] INT NULL REFERENCES [Usuarios]([Id])
);

INSERT INTO [Auditorias] ([Descripcion], [Fecha], [Usuario])
VALUES (N'El usuario Administrador cargó los datos iniciales', GETDATE(), 1);

INSERT INTO [Auditorias] ([Descripcion], [Fecha], [Usuario])
VALUES (N'El usuario Usuario de prueba inició sesión', GETDATE(), 2);

-- Claves de ejemplo:  admin@academia.com -> Admin123   |   usuario@academia.com -> Usuario123
