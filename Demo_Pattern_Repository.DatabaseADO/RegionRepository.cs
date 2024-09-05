using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_Pattern_Repository.DatabaseADO
{
    public class RegionRepository : IRegionRepository
    {

        private const string connectionString = @"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True";

        private Region Mapper(IDataRecord record)
        {
            return new Region()
            {
                Id = (int)record["Id"],
                Name = (string)record["Name"],
            };
        }

        public Region Add(Region region)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO [Region]([Name])"
                                + " OUTPUT [inserted].*"
                                + " VALUES (@name)";
            command.Parameters.AddWithValue("name", region.Name);

            Region regionInserted;

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                regionInserted = Mapper(reader);
            }
            connection.Close();

            return regionInserted;
        }

        public IEnumerable<Region> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM [Region]";

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Mapper(reader);
                }
            }
            connection.Close();
        }


        public IEnumerable<Region> GetRegionOfAnimal(Animal animal)
        {
            return GetRegionOfAnimal(animal.Id);
        }
        public IEnumerable<Region> GetRegionOfAnimal(int animalId)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT [Region].* " +
                                  "FROM [Region] " +
                                  " JOIN [Animal_Region] [AR] ON [AR].RegionId = Id " +
                                  "WHERE [AR].AnimalId = @AnimalId";
            command.Parameters.AddWithValue("AnimalId", animalId);

            connection.Open();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return Mapper(reader);
                }
            }

            connection.Close();
        }
    }
}
