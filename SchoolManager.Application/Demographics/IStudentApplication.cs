using SchoolManager.Models.Demographics;

namespace SchoolManager.Application.Demographics;

public interface IStudentApplication
{
    int CreateStudent(Student student);
    IList<Student> GetAllStudents();
    Student GetById(int id);
}

