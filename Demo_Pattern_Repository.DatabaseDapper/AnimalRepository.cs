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

            connection.Open();
            Animal animalInserted = connection.QuerySingle<Animal>(
                "INSERT INTO [Animal]([Name], [Domesticated], [LifeExpectancy], [FamiliaId])" +
                " OUTPUT [inserted].*" +
                " VALUES (@Name, @IsDomesticated, @LifeExpectancy, @FamiliaId)",
                new { animal.Name, animal.IsDomesticated, animal.LifeExpectancy, FamiliaId = animal.Familia.Id }
            );
            connection.Close();

            return animalInserted;
        }

        public IEnumerable<Animal> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();
            foreach (Animal animal in connection.Query<Animal, Familia, Animal>(
                "SELECT [Animal].[Id], [Animal].[Name], [Animal].[Domesticated], [Animal].[LifeExpectancy], [Animal].[FamiliaId], " +
                "   [Familia].[Id], [Familia].[Name], [Familia].[Description] AS [Desc]" +
                "FROM [Animal]" +
                " JOIN [Familia] ON [Animal].[FamiliaId] = [Familia].[Id]", (animal, familia) => {
                    animal.Familia = familia;
                    return animal;
                }))
            {
                yield return animal;
            }
            connection.Close();
        }

        public IEnumerable<Animal> GetByFamilia(string familia)
        {
            throw new NotImplementedException();
        }

        public Animal? GetById(int id)
        {
            throw new NotImplementedException();
        }


        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
