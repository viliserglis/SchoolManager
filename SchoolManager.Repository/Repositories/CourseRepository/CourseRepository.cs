using Dapper;
using SchoolManager.Models.Academic;
using SchoolManager.Repository.Constants;
using SchoolManager.Repository.Infrastructure;
using SqlKata;
using SqlKata.Compilers;

namespace SchoolManager.Repository.Repositories.CourseRepository;

public class CourseRepository(IConnectionFactory connectionFactory) : ICourseRepository
{
    private readonly string[] _columns =
    {
        $"{ColumnConstants.Id} as {nameof(Course.ID)}",
        $"{ColumnConstants.Name} as {nameof(Course.Name)}",
        $"{ColumnConstants.Room} as {nameof(Course.Room)}",
        $"{ColumnConstants.Capacity} as {nameof(Course.Capacity)}",
        $"{ColumnConstants.TeacherId} as {nameof(Course.TeacherId)}",
    };
    
    public int CreateCourse(Course course)
    {
        var query = new Query(TableConstants.Course.WithSchema(SchemaConstants.Academic));
        var data = new Dictionary<string, object>()
        {
            [ColumnConstants.Name] = course.Name,
            [ColumnConstants.LastName] = course.Room,
            [ColumnConstants.Capacity] = course.Capacity,
            [ColumnConstants.TeacherId] = course.TeacherId,
        };
        query.AsInsert(data);

        var sql = new PostgresCompiler().Compile(query);
        
        using var connection = connectionFactory.GetConnection();

        connection.Execute(sql.Sql, sql.NamedBindings);
        
        return course.ID;
    }

    public IList<Course> GetAllCourses()
    {
        var query = new Query(TableConstants.Course.WithSchema(SchemaConstants.Academic));
        query.Select(_columns);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.Query<Course>(sql.Sql);
        return result.ToList();
    }

    public Course GetById(int id)
    {
        var query = new Query(TableConstants.Course.WithSchema(SchemaConstants.Academic));
        query.Select(_columns);
        query.Where(ColumnConstants.Id, id);
        var sql = new PostgresCompiler().Compile(query);
        using var connection = connectionFactory.GetConnection();
        var result = connection.QueryFirst<Course>(sql.Sql, sql.NamedBindings);
        return result;
    }
} 