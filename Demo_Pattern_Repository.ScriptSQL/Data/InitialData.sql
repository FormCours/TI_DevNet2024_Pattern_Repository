SET IDENTITY_INSERT [Familia] ON;
INSERT INTO [Familia] ([Id], [Name], [Description])
 VALUES (1,'Canidae', NULL),
		(2, 'Felidae', 'Les Félidés (Felidae) ou Félins sont une famille de mammifères placentaires de l''ordre des carnivores et du sous-ordre des féliformes. Si on exclut le taxon fossile des Proailurinae, qui ne possède qu''un seul et unique genre connu, on y distingue trois sous-familles : les « petits félins » (Felinae), les « grands félins » (Pantherinae) et les félins dit « à dents de sabre » (Machairodontinae), aujourd''hui éteints.'),
		(3,'Ursidae', NULL),
		(4,'Bovidae', NULL),
		(5,'Equidae', NULL),
		(6,'Cervidae', NULL),
		(7,'Hominidae', NULL),
		(8,'Leporidae', NULL);
SET IDENTITY_INSERT [Familia] OFF;

INSERT INTO [dbo].[Animal] ([Name], [Domesticated], [LifeExpectancy], [FamiliaId])
 VALUES ('Dog', 1, 13, 1),
		('Wolf', 0, 8, 1),
		('Fox', 0, 5, 1),
		('Cat', 1, 15, 2),
		('Lion', 0, 14, 2),
		('Tiger', 0, 20, 2),
		('Grizzly Bear', 0, 25, 3),
		('Polar Bear', 0, 20, 3),
		('Panda', 0, 20, 3),
		('Cow', 1, 20, 4),
		('Goat', 1, 15, 4),
		('Sheep', 1, 12, 4),
		('Horse', 1, 25, 5),
		('Zebra', 0, 20, 5),
		('Donkey', 1, 30, 5),
		('Elk', 0, 20, 6),
		('Moose', 0, 15, 6),
		('Reindeer', 0, 18, 6),
		('Human', 1, 79, 7),
		('Chimpanzee', 0, 40, 7),
		('Gorilla', 0, 35, 7),
		('Rabbit', 1, 9, 8),
		('Hare', 0, 12, 8),
		('Pika', 0, 7, 8);