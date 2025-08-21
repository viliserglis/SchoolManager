using SchoolManager.Models.Academic;
using SchoolManager.Models.DTO.Academic;

namespace SchoolManager.Application.Academic;

public interface ICourseApplication
{
    int CreateCourse(Course course);
    IList<Course> GetAllCourses();
    CourseDTO GetById(int id);
} 