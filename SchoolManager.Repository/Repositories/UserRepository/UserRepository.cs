using Dapper;
using SchoolManager.Models.auth;
using SchoolManager.Repository.Constants;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.UserRepository;

public class UserRepository(IConnectionFactory connectionFactory) : IUserRepository
{
    private readonly string[] _columns =
    {
        $"{ColumnConstants.Id} as {nameof(User.Id)}",
        $"{ColumnConstants.Username} as {nameof(User.Username)}",
        $"{ColumnConstants.Password} as {nameof(User.Password)}"
    };

    public User GetByUsername(string username)
    {
        var query = new Query("auth.users");
        query.Select(_columns);
        query.Where(ColumnConstants.Username, username);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.QueryFirstOrDefault<User>(sql.Sql, sql.NamedBindings);
        return result;
    }
}
