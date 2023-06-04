USE master
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MOVIES_DEV')
BEGIN
  CREATE DATABASE MOVIES_DEV;
END;
GO

USE MOVIES_DEV;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Actors] (
        [ActorId] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Birthday] datetime2 NULL,
        CONSTRAINT [PK_Actors] PRIMARY KEY ([ActorId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Clients] (
        [ClientId] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Birthday] datetime2 NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY ([ClientId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Employees] (
        [EmployeeId] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Salary] real NULL,
        [City] nvarchar(max) NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Movies] (
        [MovieId] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Year] int NOT NULL,
        [AgeRestriction] int NOT NULL,
        [Price] real NOT NULL,
        CONSTRAINT [PK_Movies] PRIMARY KEY ([MovieId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Copies] (
        [CopyId] int NOT NULL IDENTITY,
        [Available] bit NOT NULL,
        [MovieId] int NOT NULL,
        CONSTRAINT [PK_Copies] PRIMARY KEY ([CopyId]),
        CONSTRAINT [FK_Copies_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([MovieId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Starrings] (
        [ActorId] int NOT NULL,
        [MovieId] int NOT NULL,
        CONSTRAINT [PK_Starrings] PRIMARY KEY ([ActorId], [MovieId]),
        CONSTRAINT [FK_Starrings_Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actors] ([ActorId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Starrings_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([MovieId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE TABLE [Rentals] (
        [CopyId] int NOT NULL,
        [ClientId] int NOT NULL,
        [DateOfRental] datetime2 NULL,
        [DateOfReturn] datetime2 NULL,
        CONSTRAINT [PK_Rentals] PRIMARY KEY ([ClientId], [CopyId]),
        CONSTRAINT [FK_Rentals_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([ClientId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Rentals_Copies_CopyId] FOREIGN KEY ([CopyId]) REFERENCES [Copies] ([CopyId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MovieId', N'AgeRestriction', N'Price', N'Title', N'Year') AND [object_id] = OBJECT_ID(N'[Movies]'))
        SET IDENTITY_INSERT [Movies] ON;
    EXEC(N'INSERT INTO [Movies] ([MovieId], [AgeRestriction], [Price], [Title], [Year])
    VALUES (1, 12, CAST(10 AS real), N''Star Wars Episode IV: A New Hope'', 1979),
    (2, 12, CAST(5.5 AS real), N''Ghostbusters'', 1984),
    (3, 15, CAST(8.5 AS real), N''Terminator'', 1984),
    (4, 17, CAST(5 AS real), N''Taxi Driver'', 1976),
    (5, 18, CAST(5 AS real), N''Platoon'', 1986),
    (6, 15, CAST(8.5 AS real), N''Frantic'', 1988),
    (7, 13, CAST(9.5 AS real), N''Ronin'', 1998),
    (8, 16, CAST(10.5 AS real), N''Analyze This'', 1999),
    (9, 16, CAST(8.5 AS real), N''Leon: the Professional'', 1994),
    (10, 13, CAST(8.5 AS real), N''Mission Impossible'', 1996)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MovieId', N'AgeRestriction', N'Price', N'Title', N'Year') AND [object_id] = OBJECT_ID(N'[Movies]'))
        SET IDENTITY_INSERT [Movies] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE INDEX [IX_Copies_MovieId] ON [Copies] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE INDEX [IX_Rentals_CopyId] ON [Rentals] ([CopyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    CREATE INDEX [IX_Starrings_MovieId] ON [Starrings] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230528124517_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230528124517_Initial', N'6.0.15');
END;
GO

COMMIT;
GO


