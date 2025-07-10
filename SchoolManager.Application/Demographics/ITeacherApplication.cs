using SchoolManager.Models.Demographics;

namespace SchoolManager.Application.Demographics;

public interface ITeacherApplication
{
    int CreateTeacher(Teacher teacher);
    IList<Teacher> GetAllTeachers();
}