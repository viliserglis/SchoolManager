using Dapper;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.TeacherRepository;

public class TeacherRepository(IConnectionFactory connectionFactory) : ITeacherRepository
{
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
        throw new NotImplementedException();
    }
}