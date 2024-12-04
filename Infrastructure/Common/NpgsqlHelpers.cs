using Npgsql;

namespace Infrastructure.Common;

public class NpgsqlHelpers
{
       public NpgsqlHelpers(string connectionString)
       {
              var connection = new NpgsqlConnection(connectionString);
              connection.Open();
       }
}