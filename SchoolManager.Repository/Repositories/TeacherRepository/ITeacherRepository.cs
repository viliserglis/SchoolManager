using SchoolManager.Models.Demographics;

namespace SchoolManager.Repository.Repositories.TeacherRepository;

public interface ITeacherRepository
{
    int  CreateTeacher(Teacher teacher);
    IList<Teacher> GetAllTeachers();
    Teacher GetById(int id);
}