CREATE TABLE [dbo].[Animal_Region]
(
	[AnimalId] INT,
	[RegionId] INT,
	
	CONSTRAINT [PK_Animal_Region] PRIMARY KEY([AnimalId], [RegionId]),
	CONSTRAINT [FK_Animal_Region__Animal] FOREIGN KEY([AnimalId]) REFERENCES [Animal]([Id]),
	CONSTRAINT [FK_Animal_Region__Region] FOREIGN KEY([RegionId]) REFERENCES [Region]([Id])
)
