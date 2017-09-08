IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170822015205_AddStockPhotoEntity')
BEGIN
    CREATE TABLE [StockPhoto] (
        [Id] int NOT NULL IDENTITY,
        [Category] nvarchar(256) NULL,
        [ImportedDate] datetime2 NOT NULL,
        [PageUrl] nvarchar(256) NULL,
        [PublishedDate] nvarchar(256) NULL,
        [Rating] nvarchar(256) NULL,
        [UniqueId] nvarchar(256) NOT NULL,
        [OEmbedInfo_AuthorName] nvarchar(256) NULL,
        [OEmbedInfo_AuthorUrl] nvarchar(256) NULL,
        [OEmbedInfo_Height] int NOT NULL,
        [OEmbedInfo_ProviderName] nvarchar(256) NULL,
        [OEmbedInfo_ProviderUrl] nvarchar(256) NULL,
        [OEmbedInfo_ThumbnailHeight] int NOT NULL,
        [OEmbedInfo_ThumbnailUrl] nvarchar(256) NULL,
        [OEmbedInfo_ThumbnailWidth] int NOT NULL,
        [OEmbedInfo_Title] nvarchar(256) NULL,
        [OEmbedInfo_Type] nvarchar(256) NULL,
        [OEmbedInfo_Url] nvarchar(256) NULL,
        [OEmbedInfo_Version] nvarchar(256) NULL,
        [OEmbedInfo_Width] int NOT NULL,
        CONSTRAINT [PK_StockPhoto] PRIMARY KEY ([Id]),
        CONSTRAINT [AK_StockPhoto_UniqueId] UNIQUE ([UniqueId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170822015205_AddStockPhotoEntity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170822015205_AddStockPhotoEntity', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_AuthorName');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_AuthorName];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_AuthorUrl');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_AuthorUrl];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_Height');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_Height];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_ProviderName');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_ProviderName];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_ProviderUrl');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_ProviderUrl];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_ThumbnailHeight');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_ThumbnailHeight];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_ThumbnailUrl');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_ThumbnailUrl];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_ThumbnailWidth');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_ThumbnailWidth];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_Type');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_Type];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_Url');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_Url];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_Version');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_Version];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'StockPhoto') AND [c].[name] = N'OEmbedInfo_Width');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [StockPhoto] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [StockPhoto] DROP COLUMN [OEmbedInfo_Width];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    EXEC sp_rename N'StockPhoto.OEmbedInfo_Title', N'Title', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    ALTER TABLE [StockPhoto] ADD [ContentUrl] nvarchar(256) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    ALTER TABLE [StockPhoto] ADD [Copyright] nvarchar(256) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    ALTER TABLE [StockPhoto] ADD [Description] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    CREATE TABLE [Thumbnail] (
        [Id] int NOT NULL IDENTITY,
        [Height] int NOT NULL,
        [PhotoId] int NULL,
        [Url] nvarchar(512) NULL,
        [Width] int NOT NULL,
        CONSTRAINT [PK_Thumbnail] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Thumbnail_StockPhoto_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [StockPhoto] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    CREATE INDEX [IX_Thumbnail_PhotoId] ON [Thumbnail] ([PhotoId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830025407_Update StockPhoto entity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170830025407_Update StockPhoto entity', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830030338_Add ContentWidth and ContentHeight to StockPhoto')
BEGIN
    ALTER TABLE [StockPhoto] ADD [ContentHeight] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830030338_Add ContentWidth and ContentHeight to StockPhoto')
BEGIN
    ALTER TABLE [StockPhoto] ADD [ContentWidth] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170830030338_Add ContentWidth and ContentHeight to StockPhoto')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170830030338_Add ContentWidth and ContentHeight to StockPhoto', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170906030443_Add IsDeleted to StockPhoto')
BEGIN
    ALTER TABLE [StockPhoto] ADD [IsDeleted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170906030443_Add IsDeleted to StockPhoto')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170906030443_Add IsDeleted to StockPhoto', N'2.0.0-rtm-26452');
END;

GO

