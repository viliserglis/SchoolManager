using SchoolManager.Models.Academic;
using SchoolManager.Models.Demographics;

namespace SchoolManager.Repository.Repositories.StudentCourseMapRepository;

public interface IStudentCourseMap
{ 
    void Create(int student, int course);
    int[] GetStudentIds(int course);
    int[] GetCourseIds(int student);
}