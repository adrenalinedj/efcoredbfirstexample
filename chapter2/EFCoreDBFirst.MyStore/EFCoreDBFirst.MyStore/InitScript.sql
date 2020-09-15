CREATE DATABASE [EFCoreDBFirstMyStore];
GO

USE [EFCoreDBFirstMyStore];
GO

CREATE TABLE [dbo].[Category]
(
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(255) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL IDENTITY,
	[CategoryId] INT NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Price] DECIMAL(18, 3) NOT NULL,
	[Stock] INT NOT NULL,
	[CreatedDate] DATETIME NOT NULL,
	[ModifiedDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Product_Category_Id] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);
GO

CREATE SCHEMA [stats]
GO

CREATE TABLE [stats].[ProductView]
(
	[Id] INT NOT NULL IDENTITY,
	[ProductId] INT NOT NULL,
	[Views] INT NOT NULL,
	CONSTRAINT [PK_ProductView] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_ProductView_Product_Id] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);
GO
