using SchoolManager.Models.Demographics;

namespace SchoolManager.Application.Demographics;

public interface ITeacherApplication
{
    int CreateTeacher(Teacher eacher);
    IList<Teacher> GetAllTeachers();
}