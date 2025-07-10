using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Repositories.TeacherRepository;

namespace SchoolManager.Application.Demographics;

public class TeacherApplication(ITeacherRepository teacherRepository) : ITeacherApplication
{
    public int CreateTeacher(Teacher teacher)
    {
        return teacherRepository.CreateTeacher(teacher);
    }

    public IList<Teacher> GetAllTeachers()
    {
        return teacherRepository.GetAllTeachers();
    }
    
}