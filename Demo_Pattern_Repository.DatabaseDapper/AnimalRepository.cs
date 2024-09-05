using Dapper;
using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.Data.SqlClient;

namespace Demo_Pattern_Repository.DatabaseDapper
{
    public class AnimalRepository : IAnimalRepository
    {
        private const string connectionString = @"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True";

        public Animal Add(Animal animal)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            return connection.QuerySingle<Animal>(
                "INSERT INTO [Animal]([Name], [Domesticated], [LifeExpectancy], [FamiliaId])" +
                " OUTPUT [inserted].*" +
                " VALUES (@Name, @IsDomesticated, @LifeExpectancy, @FamiliaId)",
                new { animal.Name, animal.IsDomesticated, animal.LifeExpectancy, FamiliaId = animal.Familia.Id }
            );
        }

        public IEnumerable<Animal> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            IEnumerable<Animal> animals = connection.Query<Animal, Familia, Animal>(
                "SELECT [Animal].[Id], [Animal].[Name], " +
                " [Animal].[Domesticated] AS [IsDomesticated], " +
                " [Animal].[LifeExpectancy], [Animal].[FamiliaId], " +
                " [Familia].[Id], [Familia].[Name], [Familia].[Description] AS [Desc]" +
                "FROM [Animal]" +
                " JOIN [Familia] ON [Animal].[FamiliaId] = [Familia].[Id]",
                (animal, familia) =>
                {
                    animal.Familia = familia;
                    return animal;
                }
            );

            foreach (Animal animal in animals)
            {
                yield return animal;
            }
        }

        public IEnumerable<Animal> GetByFamilia(string familia)
        {
            throw new NotImplementedException();
        }

        public Animal? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            string request = "SELECT [Animal].[Id], [Animal].[Name], " + 
                              " [Animal].[Domesticated] AS [IsDomesticated], " + 
                              " [Animal].[LifeExpectancy], [Animal].[FamiliaId], " + 
                              " [Familia].[Id], [Familia].[Name], " +
                              " [Familia].[Description] AS [Desc], " +
                              " [Region].[Id], [Region].[Name] " +
                              "FROM [Animal] " +
                              " JOIN [Familia] ON [Animal].[FamiliaId] = [Familia].[Id] " +
                              " JOIN [Animal_Region] [AR] ON [Animal].[Id] = [AR].[AnimalId] " +
                              " JOIN [Region] ON [Region].[Id] = [AR].[RegionId] " +
                              "WHERE [Animal].[Id] = @AnimalId";

            IEnumerable<Animal> result = connection.Query<Animal, Familia, Region, Animal>(
                request,
                (animal, familia, region) =>
                {
                    animal.Familia = familia;
                    animal.Regions = [ region ];
                    return animal;
                },
                new { AnimalId = id});

            Animal animal = result.GroupBy(a => a.Id)
                                  .Select(grp =>
                                  {
                                      Animal a = grp.First();
                                      a.Regions = grp.Select(g => g.Regions.Single()).ToList();
                                      return a;
                                  })
                                  .Single();

            return animal;
        }

        public IEnumerable<Animal> GetFromRegion(string region)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            IEnumerable<Animal> animals = connection.Query<Animal, Familia, Animal>(
                "SELECT [Animal].[Id], [Animal].[Name], " +
                " [Animal].[Domesticated] AS [IsDomesticated], " +
                " [Animal].[LifeExpectancy], [Animal].[FamiliaId], " +
                " [Familia].[Id], [Familia].[Name], [Familia].[Description] AS [Desc]" +
                "FROM [Animal]" +
                " JOIN [Familia] ON [Animal].[FamiliaId] = [Familia].[Id]" +
                " JOIN [Animal_Region] AS [AR] ON [AR].[AnimalId] = [Animal].[Id] " +
                " JOIN [Region] ON [AR].[RegionId] = [Region].[Id] " +
                "WHERE [Region].[Name] = @Region",
                (animal, familia) =>
                {
                    animal.Familia = familia;
                    return animal;
                },
                new { Region = region }
            );

            foreach (Animal animal in animals)
            {
                yield return animal;
            }
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
