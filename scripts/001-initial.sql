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
    [Complement] varchar(250) NULL,
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
VALUES (N'20220205160029_InitialMigration', N'6.0.1');
GO

COMMIT;
GO

INSERT INTO Providers(Id, Name, Document, ProviderKind, Active)
VALUES('077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'OReilly', '35257355000145',	2, 1)

INSERT INTO Addresses(Id, ProviderId, Street, Number, Complement, ZipCode, District, City, [State])
VALUES('f22d9677-14ef-4974-0ead-08d9e8d5a0a2', '077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'Rua Tiradentes','80', NULL,	'16440970', 'Centro', 'Sabino', 'SP')

INSERT INTO Products(Id, ProviderId, Name, [Description], [Image], [Value], RegistrationDate, Active)
VALUES('c83f8fa1-c06f-4710-06a9-08d9e8d5e002', '077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'jQuery', 'Learn now how to use jQuery framework', 'b53a132f-eff4-4730-9a51-780d0060d745_1dbbd291-279f-4743-a781-0c92a482bc2b_JQuery.jpg', 20.00, '0001-01-01 00:00:00.0000000', 1),
('1c16bd29-9413-4ff7-06aa-08d9e8d5e002', '077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'CSS and Documents',	'Learn how to use and become a CSS master',	'5227e0df-bbb4-4833-8f11-54e0fab27e1a_1e723e03-a406-442b-b2c8-f4c561681379_CSS.jpg', 10.00,	'0001-01-01 00:00:00.0000000',	1),
('9aeaebf4-8386-45ff-06ab-08d9e8d5e002', '077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'ASP.NET MVC 5 with Bootstrap',	'Become a great developer using ASP.NET MVC 5 + Bootstrap',	'48b2f995-e8cd-43ad-b3bf-ffb11ba353f5_9e2b29e2-f7e6-4fbf-8696-761ebd462f47_MVC5.jpg', 55.00, '0001-01-01 00:00:00.0000000',	1),
('7a32475e-9dcc-4ec1-06ac-08d9e8d5e002', '077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'HTML5',	'Learn how to build awesome web sites using HTML5',	'9c1f453f-8120-4b10-9307-41e413af210e_734ed7ec-23c7-4b6a-80ea-9cbf445a6729_HTML.jpg', 23.00, '0001-01-01 00:00:00.0000000',	1),
('b5fb7f0c-b12d-4054-06ad-08d9e8d5e002', '077cc9fd-4475-4a1f-296b-08d9e8d5a09f', 'Razor',	'Become a razor master with this amazing e-book',	'6e1fb36c-1c6d-4852-9d79-a534ae73806c_cc0a711a-a172-48a1-ab69-b074b572321b_Razor.jpg',	105.00,	'0001-01-01 00:00:00.0000000',	1)