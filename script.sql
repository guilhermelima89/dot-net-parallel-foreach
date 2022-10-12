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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221012171455_001')
BEGIN
    CREATE TABLE [Cliente] (
        [Id] int NOT NULL IDENTITY,
        [Nome] varchar(250) NOT NULL,
        [DataCadastro] datetime NULL DEFAULT (getdate()),
        CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221012171455_001')
BEGIN
    CREATE TABLE [Produto] (
        [Id] int NOT NULL IDENTITY,
        [Descricao] varchar(250) NOT NULL,
        [DataCadastro] datetime NULL DEFAULT (getdate()),
        CONSTRAINT [PK_Produto] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221012171455_001')
BEGIN
    CREATE TABLE [Pedido] (
        [Id] int NOT NULL IDENTITY,
        [ClienteId] int NOT NULL,
        [ProdutoId] int NOT NULL,
        [DataCadastro] datetime NULL DEFAULT (getdate()),
        CONSTRAINT [PK_Pedido] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pedido_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]),
        CONSTRAINT [FK_Pedido_Produto_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produto] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221012171455_001')
BEGIN
    CREATE INDEX [IX_Pedido_ClienteId] ON [Pedido] ([ClienteId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221012171455_001')
BEGIN
    CREATE INDEX [IX_Pedido_ProdutoId] ON [Pedido] ([ProdutoId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221012171455_001')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221012171455_001', N'6.0.4');
END;
GO

COMMIT;
GO

