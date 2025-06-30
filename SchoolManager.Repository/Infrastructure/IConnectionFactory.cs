using System.Data;

namespace SchoolManager.Repository.Infrastructure;

public interface IConnectionFactory
{
    IDbConnection GetConnection();
}