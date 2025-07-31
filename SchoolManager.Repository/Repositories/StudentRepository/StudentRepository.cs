using Dapper;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.StudentRepository;

public class StudentRepository(IConnectionFactory connectionFactory) : IStudentRepository
{

    private readonly string[] _columms =
    {

         $"id as {nameof(Student.Id)}",
         $"first_name as {nameof(Student.FirstName)}",
         $"last_name as {nameof(Student.LastName)}",
    };
    public int CreateStudent(Student student)
    {
        var query = new Query("demographics.students");
        var data = new Dictionary<string, object>()
        {
            ["first_name"] = student.FirstName,
            ["last_name"] = student.LastName,
        };

        query.AsInsert(data);

        var sql = new PostgresCompiler().Compile(query);
        
        using var connection = connectionFactory.GetConnection();

        connection.Execute(sql.Sql, sql.NamedBindings);
        
        return student.Id;
            
    }

    public IList<Student> GetAllStudents()
    {
        var query = new Query("demographics.students");
        query.Select(_columms);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<Student>(sql.Sql);
        return result.ToList();
    }
    
    public Student GetById(int id)
    {
        var query = new Query("demographics.students");
        query.Select(_columms);
        query.Where("id", id);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.QueryFirst<Student>(sql.Sql, sql.NamedBindings);
        return result;
    }
}