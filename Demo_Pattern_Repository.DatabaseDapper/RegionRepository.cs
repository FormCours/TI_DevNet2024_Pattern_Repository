using Dapper;
using Demo_Pattern_Repository.Models;
using Demo_Pattern_Repository.Repositories;
using Microsoft.Data.SqlClient;

namespace Demo_Pattern_Repository.DatabaseDapper
{
    public class RegionRepository : IRegionRepository
    {
        private const string connectionString = @"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True";

        public Region Add(Region region)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            Region regionInserted = connection.QuerySingle<Region>(
                "INSERT INTO [Region]([Name])" +
                " OUTPUT [inserted].*  " +
                " VALUES (@Name)",
                region
            );
            connection.Close();

            return regionInserted;
        }

        public IEnumerable<Region> GetAll()
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            foreach (Region region in connection.Query<Region>("SELECT [Id], [Name] FROM [Region]"))
            {
                yield return region;
            }
            connection.Close();
        }
    }
}
