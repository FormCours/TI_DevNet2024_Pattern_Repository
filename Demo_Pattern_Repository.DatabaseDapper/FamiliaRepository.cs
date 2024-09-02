using Dapper;
using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.Data.SqlClient;

namespace Demo_Pattern_Repository.DatabaseDapper
{
    public class FamiliaRepository : IFamiliaRepository
    {
        private const string connectionString = @"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True";

        public Familia? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            Familia? familia = connection.QuerySingleOrDefault<Familia>(
                "SELECT [Id], [Name], [Description] AS [Desc] FROM [Familia] WHERE [Id] = @Id",
                new { Id = id }
            );

            connection.Close();
            return familia;
        }

        public IEnumerable<Familia> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            foreach (Familia familia in connection.Query<Familia>("SELECT [Id], [Name], [Description] AS [Desc] FROM [Familia]"))
            {
                yield return familia;
            }
            connection.Close();
        }

        public Familia Add(Familia familia)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            Familia familiaInserted = connection.QuerySingle<Familia>(
                "INSERT INTO [Familia]([Name], [Description])" +
                " OUTPUT [inserted].[Id], [inserted].[Name], [inserted].[Description] AS [Desc]  " +
                " VALUES (@Name, @Desc)",
                new
                {
                    Name = familia.Name,
                    Desc = string.IsNullOrWhiteSpace(familia.Desc) ? null : familia.Desc
                }
            );
            connection.Close();
            
            return familiaInserted;
        }
    }
}
