using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Repositories.StudentRepository;

namespace SchoolManager.Application.Demographics;

public class StudentApplication(IStudentRepository studentRepository) : IStudentApplication
{
    public int CreateStudent(Student student)
    {
        return studentRepository.CreateStudent(student);
    }

    public IList<Student> GetAllStudents()
    {
        return studentRepository.GetAllStudents();
    }
    
    public Student GetById(int id)
    {
        return studentRepository.GetById(id);
    }
    
}
