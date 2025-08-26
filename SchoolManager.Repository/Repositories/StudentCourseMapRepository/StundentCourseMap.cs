using Dapper;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.StudentCourseMapRepository;

public class StundentCourseMap(IConnectionFactory connectionFactory) : IStudentCourseMap
{
    public void Create(int student, int course)
    {
        var query = new Query("academic.student_course");
        var data = new Dictionary<string, object>()
        {
            ["student_id"] = student,
            ["course_id"] = course
        };
        query.AsInsert(data);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        connection.Execute(sql.Sql, sql.NamedBindings);
    }

    public int[] GetStudentIds(int course)
    {
        var query = new Query("academic.student_course");
        query.Select("student_id");
        query.Where("course_id", course);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<int>(sql.Sql, sql.NamedBindings);
        return result.ToArray();
    }

    public int[] GetCourseIds(int student)
    {
        var query = new Query("academic.student_course");
        query.Select("course_id");
        query.Where("student_id", student);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<int>(sql.Sql, sql.NamedBindings);
        return result.ToArray();
    }
}