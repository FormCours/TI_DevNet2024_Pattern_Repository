using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_Pattern_Repository.DatabaseADO
{
    public class AnimalRepository : IAnimalRepository
    {
        private const string connectionString = @"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True";

        private Animal Mapper(IDataRecord record)
        {
            return new Animal()
            {
                Id = (int)record["Id"],
                Name = (string)record["Name"],
                IsDomesticated = (bool)record["Domesticated"],
                LifeExpectancy = record["LifeExpectancy"] is DBNull ? null : (int)record["LifeExpectancy"]
            };
        }
        private Animal Mapper(IDataRecord record, IEnumerable<Familia> familias)
        {
            Animal animal = Mapper(record);
            animal.Familia = familias.Single(f => f.Id == (int)record["familiaId"]);
            return animal;
        }
        private Animal MapperWithFamilia(IDataRecord record)
        {
            Animal animal = Mapper(record);
            animal.Familia = new Familia()
            {
                Id = (int)record["Familia.Id"],
                Name = (string)record["Familia.Name"],
                Desc = record["Familia.Desc"] is DBNull ? null : (string)record["Familia.Desc"]
            };
            return animal;
        }

        public Animal Add(Animal animal)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO [Animal]([Name], [Domesticated], [LifeExpectancy], [FamiliaId])"
                                + " OUTPUT [inserted].*"
                                + " VALUES (@name, @domesticated, @life_expectancy, @familiaid)";
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("domesticated", animal.IsDomesticated);
            command.Parameters.AddWithValue("life_expectancy", (object?)animal.LifeExpectancy ?? DBNull.Value);
            command.Parameters.AddWithValue("familiaid", animal.Familia.Id);

            Animal animalInserted;

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                animalInserted = Mapper(reader);
            }
            connection.Close();

            return animalInserted;
        }

        public IEnumerable<Animal> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            IEnumerable<Familia> familias = (new FamiliaRepository()).GetAll().ToList();

            command.CommandText = "SELECT * FROM [Animal]";

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Mapper(reader, familias);
                }
            }
            connection.Close();
        }

        public IEnumerable<Animal> GetByFamilia(string familia)
        {
            throw new NotImplementedException();
        }

        public Animal? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT [Animal].*, " +
                                  " [Familia].[Id] AS [Familia.Id]," +
                                  " [Familia].[Name] AS [Familia.Name], " +
                                  " [Familia].[Description] AS [Familia.Desc] " +
                                  "FROM [Animal] " +
                                  " JOIN [Familia] ON [Animal].[FamiliaId] = [Familia].[Id]" +
                                  "WHERE [Animal].[Id] = @Id";
            command.Parameters.AddWithValue("Id", id);

            Animal? animal = null;
            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if(reader.Read())
                {
                    animal = MapperWithFamilia(reader);
                }
            }

            connection.Close();

            if(animal is not null)
            {
                animal.Regions = (new RegionRepository()).GetRegionOfAnimal(animal).ToList();
            }

            return animal;
        }

        public IEnumerable<Animal> GetFromRegion(string region)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            IEnumerable<Familia> familias = (new FamiliaRepository()).GetAll().ToList();

            command.CommandText = "SELECT * " +
                                  "FROM [Animal] " +
                                  " JOIN [Animal_Region] AS [AR] ON [AR].[AnimalId] = [Animal].[Id] " +
                                  " JOIN [Region] ON [AR].[RegionId] = [Region].[Id] " +
                                  "WHERE [Region].[Name] = @region";
            command.Parameters.AddWithValue("region", region);

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Mapper(reader, familias);
                }
            }
            connection.Close();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
