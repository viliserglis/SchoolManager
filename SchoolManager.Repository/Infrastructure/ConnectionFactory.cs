using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using SchoolManager.Repository.IoC;

namespace SchoolManager.Repository.Infrastructure;

public class ConnectionFactory(IOptions<RepositoryConfiguration> configuration) : IConnectionFactory
{
    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(configuration.Value.ConnectionString);
    }
}