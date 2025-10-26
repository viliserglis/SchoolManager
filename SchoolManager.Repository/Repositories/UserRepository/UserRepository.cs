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
        $"{ColumnConstants.Password} as {nameof(User.Password)}",
        $"{ColumnConstants.UserRole} as {nameof(User.Role)}"
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

    public IList<Permissions> GetRolePermision(Roles role)
    {
        var query = new Query(TableConstants.RolePermissions.WithSchema(SchemaConstants.Auth));
        query.Select(ColumnConstants.PermissionCode);
        query.Where(ColumnConstants.RoleCode, role);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<Permissions>(sql.Sql, sql.NamedBindings);
        return result.ToList();
    }

    public int CreateUser(User user)
    {
        var query = new Query(TableConstants.User.WithSchema(SchemaConstants.Auth));
        query.AsInsert(new Dictionary<string, object>
        {
            [ColumnConstants.Username] = user.Username,
            [ColumnConstants.Password] = user.Password,
            [ColumnConstants.UserRole] = user.Role
        });
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var newUserId = connection.ExecuteScalar<int>(sql.Sql + " RETURNING id", sql.NamedBindings);
        return newUserId;
    }

    public bool SetPassword(int userId, string newPasswordHash)
    {
        var query = new Query(TableConstants.User.WithSchema(SchemaConstants.Auth));
        query.Where(ColumnConstants.Id, userId);
        query.AsUpdate(new Dictionary<string, object>
        {
            [ColumnConstants.Password] = newPasswordHash
        });
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var rowsAffected = connection.Execute(sql.Sql, sql.NamedBindings);
        return rowsAffected > 0;
    }

    public bool UpdateUserRole(int userId, Roles newRole)
    {
        var query = new Query(TableConstants.User.WithSchema(SchemaConstants.Auth));
        query.Where(ColumnConstants.Id, userId);
        query.AsUpdate(new Dictionary<string, object>
        {
            [ColumnConstants.UserRole] = newRole
        });
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var rowsAffected = connection.Execute(sql.Sql, sql.NamedBindings);
        return rowsAffected > 0;
    }
}
    
