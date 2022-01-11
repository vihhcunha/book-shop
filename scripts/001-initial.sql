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

CREATE TABLE [Fornecedores] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Documento] varchar(14) NOT NULL,
    [TipoFornecedor] int NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Enderecos] (
    [Id] uniqueidentifier NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [Logradouro] varchar(200) NOT NULL,
    [Numero] varchar(50) NOT NULL,
    [Complemento] varchar(250) NOT NULL,
    [Cep] varchar(8) NOT NULL,
    [Bairro] varchar(100) NOT NULL,
    [Cidade] varchar(200) NOT NULL,
    [Estado] varchar(50) NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enderecos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Produtos] (
    [Id] uniqueidentifier NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Descricao] varchar(1000) NOT NULL,
    [Imagem] varchar(100) NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produtos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Enderecos_FornecedorId] ON [Enderecos] ([FornecedorId]);
GO

CREATE INDEX [IX_Produtos_FornecedorId] ON [Produtos] ([FornecedorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220110225231_Initial', N'6.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Enderecos];
GO

DROP TABLE [Produtos];
GO

DROP TABLE [Fornecedores];
GO

CREATE TABLE [Providers] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Document] varchar(14) NOT NULL,
    [ProviderKind] int NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Providers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Addresses] (
    [Id] uniqueidentifier NOT NULL,
    [ProviderId] uniqueidentifier NOT NULL,
    [Street] varchar(200) NOT NULL,
    [Number] varchar(50) NOT NULL,
    [Complement] varchar(250) NOT NULL,
    [ZipCode] varchar(8) NOT NULL,
    [District] varchar(100) NOT NULL,
    [City] varchar(200) NOT NULL,
    [State] varchar(50) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Addresses_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [ProviderId] uniqueidentifier NOT NULL,
    [Name] varchar(200) NOT NULL,
    [Description] varchar(1000) NOT NULL,
    [Image] varchar(100) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [RegistrationDate] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Addresses_ProviderId] ON [Addresses] ([ProviderId]);
GO

CREATE INDEX [IX_Products_ProviderId] ON [Products] ([ProviderId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220111113232_UpdateTableNamesAndColumns', N'6.0.1');
GO

COMMIT;
GO

