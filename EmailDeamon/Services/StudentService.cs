using Dapper;
using Npgsql;

namespace EmailDeamon.Services;

public class StudentService
{
    private readonly string _connectionString;
    private int _lastCheckedStudentId = 0;

    public StudentService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Student>> GetNewStudentsAsync()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"
            SELECT id as Id, first_name as FirstName, last_name as LastName
            FROM demographics.students
            WHERE id > @LastCheckedId
            ORDER BY id";

        var students = await connection.QueryAsync<Student>(query, new { LastCheckedId = _lastCheckedStudentId });
        var studentsList = students.ToList();

        if (studentsList.Any())
        {
            _lastCheckedStudentId = studentsList.Max(s => s.Id);
        }

        return studentsList;
    }
}

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
