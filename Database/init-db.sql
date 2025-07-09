CREATE DATABASE TestDB;
GO

USE TestDB;
GO

CREATE TABLE DocKey (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  DocName varchar(200) NOT NULL,
  [Key] varchar(200) NOT NULL
)
GO

CREATE TABLE LogProcess (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  OriginalFileName varchar(200) NOT NULL,
  Status varchar(200) NOT NULL,
  NewFileName varchar(200),
  DateProcess DATETIME2 NOT NULL
)
GO