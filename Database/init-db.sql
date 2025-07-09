IF DB_ID('TestDB') IS NULL
BEGIN
    CREATE DATABASE TestDB;
END
GO

USE TestDB;
GO

IF OBJECT_ID('DocKey', 'U') IS NULL
BEGIN
    CREATE TABLE DocKey (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        DocName VARCHAR(200) NOT NULL,
        [Key] VARCHAR(200) NOT NULL
    );
END
GO

IF OBJECT_ID('LogProcess', 'U') IS NULL
BEGIN
    CREATE TABLE LogProcess (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OriginalFileName VARCHAR(200) NOT NULL,
        Status VARCHAR(200) NOT NULL,
        NewFileName VARCHAR(200),
        DateProcess DATETIME2 NOT NULL
    );
END
GO

