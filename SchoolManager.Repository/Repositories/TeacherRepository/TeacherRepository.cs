using Dapper;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.TeacherRepository;

public class TeacherRepository(IConnectionFactory connectionFactory) : ITeacherRepository
{
    
    private readonly string[] _columms =
    {

        $"id as {nameof(Teacher.Id)}",
        $"first_name as {nameof(Teacher.FirstName)}",
        $"last_name as {nameof(Teacher.LastName)}",
        $"qualification as {nameof(Teacher.Qualification)}",
    };
    public int CreateTeacher(Teacher teacher)
    {
        var query = new Query("demographics.Teacher");
        var data = new Dictionary<string, object>()
        {
            ["first_name"] = teacher.FirstName,
            ["last_name"] = teacher.LastName,
            ["qualification"] = teacher.Qualification
        };

        query.AsInsert(data);

        var sql = new PostgresCompiler().Compile(query);
        
        using var connection = connectionFactory.GetConnection();

        connection.Execute(sql.Sql, sql.NamedBindings);
        
        return teacher.Id;
            
    }

    public IList<Teacher> GetAllTeachers()
    {
        var query = new Query("demographics.Teacher");
        query.Select(_columms);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<Teacher>(sql.Sql);
        return result.ToList();
    }
    
    public Teacher GetById(int id)
    {
        var query = new Query("demographics.Teacher");
        query.Select(_columms);
        query.Where("id", id);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.QueryFirst<Teacher>(sql.Sql, sql.NamedBindings);
        return result;
    }
}