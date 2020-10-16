CREATE DATABASE [CarDealer];
USE [CarDealer];
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/10/2020 10:37:52 ******/
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
/****** Object:  Table [dbo].[AvailibleCars]    Script Date: 16/10/2020 10:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvailibleCars](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name_Brand] [nvarchar](max) NULL,
	[Name_Model] [nvarchar](max) NULL,
	[Engine_Type] [int] NULL,
	[Engine_EuroStandard_Value] [int] NULL,
	[Engine_EngineCapacity_DisplacementInCm3] [decimal](18, 2) NULL,
	[Engine_BatteryCapacity_CapacityInKwh] [decimal](18, 2) NULL,
	[Transmission] [int] NOT NULL,
	[CurrentMileage_MileageInKm] [int] NULL,
	[BasePrice_Amount] [decimal](18, 2) NULL,
	[IsReserved] [bit] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_AvailibleCars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarHistoryItems]    Script Date: 16/10/2020 10:37:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarHistoryItems](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateOfItem] [datetime2](7) NOT NULL,
	[Mileage_MileageInKm] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[AvailibleCarId] [bigint] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[LastModified] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_CarHistoryItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CarHistoryItems]  WITH CHECK ADD  CONSTRAINT [FK_CarHistoryItems_AvailibleCars_AvailibleCarId] FOREIGN KEY([AvailibleCarId])
REFERENCES [dbo].[AvailibleCars] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarHistoryItems] CHECK CONSTRAINT [FK_CarHistoryItems_AvailibleCars_AvailibleCarId]
GO
