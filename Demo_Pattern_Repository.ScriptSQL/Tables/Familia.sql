CREATE TABLE [dbo].[Familia]
(
	[Id] INT IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(4000),

	CONSTRAINT [PK_Familia] PRIMARY KEY ([Id]),
	CONSTRAINT [UK_Familia] UNIQUE ([Name])
)
