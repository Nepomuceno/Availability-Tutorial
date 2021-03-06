-- Script Date: 31/05/2016 00:51  - ErikEJ.SqlCeScripting version 3.5.2.56
-- Database information:
-- Locale Identifier: 1033
-- Encryption Mode: 
-- Case Sensitive: False
-- Database: Products.sdf
-- ServerVersion: 4.0.8876.1
-- DatabaseSize: 84 KB
-- Created: 05/03/2016 14:36

-- User Table information:
-- Number of tables: 1
-- Products: 1 row(s)

USE master
GO

CREATE Database Products
GO

USE Products
GO

CREATE TABLE [Products] (
  [Id] int IDENTITY (2,1) NOT NULL
, [ProductName] nvarchar(100) NOT NULL
, [ProductDescription] nvarchar(250) NULL
, [ProductPrice] float NOT NULL
);
GO
SET IDENTITY_INSERT [Products] ON;
GO
INSERT INTO [Products] ([Id],[ProductName],[ProductDescription],[ProductPrice]) VALUES (1,N'Carrot Caje',N'What could be finer than carrot cake',10.5);
GO
SET IDENTITY_INSERT [Products] OFF;
GO
ALTER TABLE [Products] ADD CONSTRAINT [PK_Products] PRIMARY KEY ([Id]);
GO

