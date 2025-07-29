using Dapper;
using SchoolManager.Models.Academic;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.CourseRepository;

public class CourseRepository(IConnectionFactory connectionFactory) : ICourseRepository
{
    private readonly string[] _columns =
    {
        $"id as {nameof(Course.ID)}",
        $"name as {nameof(Course.Name)}",
        $"room as {nameof(Course.Room)}",
        $"capacity as {nameof(Course.Capacity)}"
    };
    
    public int CreateCourse(Course course)
    {
        var query = new Query("academic.courses");
        var data = new Dictionary<string, object>()
        {
            ["name"] = course.Name,
            ["room"] = course.Room,
            ["capacity"] = course.Capacity
        };

        query.AsInsert(data);

        var sql = new PostgresCompiler().Compile(query);
        
        using var connection = connectionFactory.GetConnection();

        connection.Execute(sql.Sql, sql.NamedBindings);
        
        return course.ID;
    }

    public IList<Course> GetAllCourses()
    {
        var query = new Query("academic.courses");
        query.Select(_columns);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<Course>(sql.Sql);
        return result.ToList();
    }
} 