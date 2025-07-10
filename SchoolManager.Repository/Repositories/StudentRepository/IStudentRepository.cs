using SchoolManager.Models.Demographics;

namespace SchoolManager.Repository.Repositories.StudentRepository;

public interface IStudentRepository
{
    int  CreateStudent(Student student);
    IList<Student> GetAllStudents();
}