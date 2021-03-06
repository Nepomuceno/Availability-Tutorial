-- Script Date: 31/05/2016 00:48  - ErikEJ.SqlCeScripting version 3.5.2.56
-- Database information:
-- Locale Identifier: 1033
-- Encryption Mode: 
-- Case Sensitive: False
-- Database: Orders.sdf
-- ServerVersion: 4.0.8876.1
-- DatabaseSize: 84 KB
-- Created: 05/03/2016 14:36

-- User Table information:
-- Number of tables: 1
-- Orders: 0 row(s)

USE master
GO

CREATE Database Orders
GO

USE Orders
GO

CREATE TABLE [Orders] (
  [Id] int IDENTITY (1,1) NOT NULL
, [customername] nvarchar(250) NOT NULL
, [orderdescription] nvarchar(500) NULL
, [duedate] datetime NULL
, [completiondate] datetime NULL
);
GO
SET IDENTITY_INSERT [Orders] OFF;
GO
ALTER TABLE [Orders] ADD CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]);
GO

