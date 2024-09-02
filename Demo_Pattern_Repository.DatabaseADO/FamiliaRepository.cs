using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_Pattern_Repository.DatabaseADO
{
    public class FamiliaRepository : IFamiliaRepository
    {
        private const string connectionString = @"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True";

        private Familia Mapper(IDataRecord record)
        {
            return new Familia()
            {
                Id = (int)record["Id"],
                Name = (string)record["Name"],
                Desc = record["Description"] is DBNull ? null : (string)record["Description"]
            };
        }

        public Familia? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM [Familia] WHERE [Id] = @Id";
            command.Parameters.AddWithValue("Id", id);

            Familia? familia = null;

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    familia = Mapper(reader);
                }
            }
            connection.Close();
            return familia;
        }

        public IEnumerable<Familia> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM [Familia]";

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Familia familia = Mapper(reader);

                    // Mode différé -> La valeur est renvoyé lors de l'utilisation de l'Enumerable (foreach)
                    yield return familia;
                }
            }
            connection.Close();
        }

        public Familia Add(Familia familia)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO [Familia]([Name], [Description])"
                                + " OUTPUT [inserted].*"
                                + " VALUES (@name, @desc)";
            command.Parameters.AddWithValue("name", familia.Name);
            command.Parameters.AddWithValue("desc", string.IsNullOrWhiteSpace(familia.Desc) ? DBNull.Value : familia.Desc);
            //command.Parameters.AddWithValue("desc", familia.Desc as object ?? DBNull.Value);

            Familia familiaInserted;

            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                familiaInserted = Mapper(reader);
            }
            connection.Close();

            return familiaInserted;
        }

    }
}