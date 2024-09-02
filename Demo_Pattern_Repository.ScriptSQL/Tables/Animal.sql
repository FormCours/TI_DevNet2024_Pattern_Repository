CREATE TABLE [dbo].[Animal]
(
	[Id] INT IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	[Domesticated] BIT NOT NULL,
	[LifeExpectancy] INT NULL,
	[FamiliaId] INT NOT NULL,

	CONSTRAINT [PK_Animal] PRIMARY KEY([Id]),
	CONSTRAINT [UK_Animal] UNIQUE([Name], [Domesticated]),
	CONSTRAINT [FK_Animal__Familia] FOREIGN KEY([FamiliaId]) REFERENCES [Familia]([Id])
)
