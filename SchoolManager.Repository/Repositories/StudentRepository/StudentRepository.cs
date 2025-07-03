using Dapper;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.StudentRepository;

public class StudentRepository(IConnectionFactory connectionFactory) : IStudentRepository
{
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
        throw new NotImplementedException();
    }
}