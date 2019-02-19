
use master
CREATE DATABASE ClientsDB

go
use ClientsDB
CREATE TABLE [dbo].[City] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

go
use ClientsDB
CREATE TABLE [dbo].[Client] (
    [Id]      INT           NOT NULL,
    [Name]    NVARCHAR (50) NOT NULL,
    [Surname] NVARCHAR (50) NOT NULL,
    [CityID]  INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CityIDFK] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([Id])
);

go
use ClientsDB
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (1, N'Москва')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (2, N'Петербург')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (3, N'Сыктывкар')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (4, N'Липецк')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (5, N'Тула')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (6, N'Брянск')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (7, N'Воронеж')
INSERT INTO [dbo].[City] ([Id], [Name]) VALUES (8, N'Саратов')

INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (1, N'Иван', N'Иванов', 1)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (2, N'Пётр', N'Петров', 1)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (3, N'Василий', N'Васильев', 2)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (4, N'Михаил', N'Михайлов', 3)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (5, N'Геннадий', N'Геннадиев', 4)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (6, N'Степан', N'Степанов', 4)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (7, N'Андрей', N'Андреев', 3)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (8, N'Сергей', N'Сергеев', 2)
INSERT INTO [dbo].[Client] ([Id], [Name], [Surname], [CityID]) VALUES (9, N'Дмитрий', N'Дмитриев', 1)


