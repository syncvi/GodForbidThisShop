CREATE TABLE [dbo].[Product] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Price]       MONEY         NOT NULL,
    [Description] VARCHAR (200) NOT NULL,
	[ProducerId] INT NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_ProducerId] FOREIGN KEY ([ProducerId]) REFERENCES [dbo].[Producer] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Category] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ShortName]   NVARCHAR (50)  NOT NULL,
    [LongName]    NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (200) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CategoryProduct] (
    [ProductId]  INT NOT NULL,
    [CategoryId] INT NOT NULL,
    CONSTRAINT [PK_CategoryProduct] PRIMARY KEY CLUSTERED ([ProductId] ASC, [CategoryId] ASC),
    CONSTRAINT [FK_CategoryProduct_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryProduct_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Role] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]    VARCHAR (20)  NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Producer] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [ProducerName]    VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Producer] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (50)  NOT NULL,
    [Password] NVARCHAR (150) NOT NULL,
    [Role]     NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);