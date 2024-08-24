USE [SAYSPAA_MVC_1]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actividades]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actividades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Clave] [nvarchar](50) NULL,
	[NombreActividad] [nvarchar](max) NOT NULL,
	[ObjetivoActividad] [nvarchar](max) NOT NULL,
	[CantidadProducto] [int] NOT NULL,
	[TipoProducto] [nvarchar](60) NOT NULL,
	[MedioVerificacionProducto] [nvarchar](max) NOT NULL,
	[Fecha1] [datetime2](7) NULL,
	[Fecha2] [datetime2](7) NULL,
	[Fecha3] [datetime2](7) NULL,
	[Fecha4] [datetime2](7) NULL,
	[ObjetivoClave] [bit] NOT NULL,
	[ActividadControl] [bit] NOT NULL,
	[Supuestos] [nvarchar](max) NOT NULL,
	[Acciones] [nvarchar](max) NOT NULL,
	[Especificaciones] [nvarchar](max) NULL,
	[AplicaIndicador] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
	[RowVersion] [timestamp] NULL,
	[UnidadAuditoraId] [int] NOT NULL,
	[DireccionGeneralId] [int] NOT NULL,
 CONSTRAINT [PK_Actividades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Num] [int] NOT NULL,
	[Bis] [nvarchar](60) NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Estado] [bit] NOT NULL,
	[UnidadAuditoraId] [int] NOT NULL,
	[DireccionGeneralId] [int] NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DireccionGenerals]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DireccionGenerals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cc] [int] NOT NULL,
	[Siglas] [nvarchar](100) NOT NULL,
	[Nombre_DG] [nvarchar](240) NOT NULL,
	[Estado] [bit] NOT NULL,
	[UnidadAuditoraId] [int] NOT NULL,
 CONSTRAINT [PK_DireccionGenerals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fraccions]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fraccions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Frac] [nvarchar](100) NOT NULL,
	[Bis] [nvarchar](60) NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
	[Estado] [bit] NOT NULL,
	[ArticuloId] [int] NOT NULL,
 CONSTRAINT [PK_Fraccions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rols]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rols](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Rols] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnidadAuditoras]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnidadAuditoras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cc] [int] NOT NULL,
	[Siglas] [nvarchar](60) NOT NULL,
	[Nombre_UA] [nvarchar](120) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_UnidadAuditoras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 23/08/2024 06:20:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Nombres] [nvarchar](150) NOT NULL,
	[Apellidos] [nvarchar](160) NOT NULL,
	[Estado] [bit] NOT NULL,
	[UnidadAuditoraId] [int] NOT NULL,
	[DireccionGeneralId] [int] NOT NULL,
	[RolId] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [RolId]
GO
ALTER TABLE [dbo].[Actividades]  WITH CHECK ADD  CONSTRAINT [FK_Actividades_DireccionGenerals_DireccionGeneralId] FOREIGN KEY([DireccionGeneralId])
REFERENCES [dbo].[DireccionGenerals] ([Id])
GO
ALTER TABLE [dbo].[Actividades] CHECK CONSTRAINT [FK_Actividades_DireccionGenerals_DireccionGeneralId]
GO
ALTER TABLE [dbo].[Actividades]  WITH CHECK ADD  CONSTRAINT [FK_Actividades_UnidadAuditoras_UnidadAuditoraId] FOREIGN KEY([UnidadAuditoraId])
REFERENCES [dbo].[UnidadAuditoras] ([Id])
GO
ALTER TABLE [dbo].[Actividades] CHECK CONSTRAINT [FK_Actividades_UnidadAuditoras_UnidadAuditoraId]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_DireccionGenerals_DireccionGeneralId] FOREIGN KEY([DireccionGeneralId])
REFERENCES [dbo].[DireccionGenerals] ([Id])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_DireccionGenerals_DireccionGeneralId]
GO
ALTER TABLE [dbo].[Articulos]  WITH CHECK ADD  CONSTRAINT [FK_Articulos_UnidadAuditoras_UnidadAuditoraId] FOREIGN KEY([UnidadAuditoraId])
REFERENCES [dbo].[UnidadAuditoras] ([Id])
GO
ALTER TABLE [dbo].[Articulos] CHECK CONSTRAINT [FK_Articulos_UnidadAuditoras_UnidadAuditoraId]
GO
ALTER TABLE [dbo].[DireccionGenerals]  WITH CHECK ADD  CONSTRAINT [FK_DireccionGenerals_UnidadAuditoras_UnidadAuditoraId] FOREIGN KEY([UnidadAuditoraId])
REFERENCES [dbo].[UnidadAuditoras] ([Id])
GO
ALTER TABLE [dbo].[DireccionGenerals] CHECK CONSTRAINT [FK_DireccionGenerals_UnidadAuditoras_UnidadAuditoraId]
GO
ALTER TABLE [dbo].[Fraccions]  WITH CHECK ADD  CONSTRAINT [FK_Fraccions_Articulos_ArticuloId] FOREIGN KEY([ArticuloId])
REFERENCES [dbo].[Articulos] ([Id])
GO
ALTER TABLE [dbo].[Fraccions] CHECK CONSTRAINT [FK_Fraccions_Articulos_ArticuloId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_DireccionGenerals_DireccionGeneralId] FOREIGN KEY([DireccionGeneralId])
REFERENCES [dbo].[DireccionGenerals] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_DireccionGenerals_DireccionGeneralId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Rols_RolId] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rols] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Rols_RolId]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_UnidadAuditoras_UnidadAuditoraId] FOREIGN KEY([UnidadAuditoraId])
REFERENCES [dbo].[UnidadAuditoras] ([Id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_UnidadAuditoras_UnidadAuditoraId]
GO
