using SchoolManager.Models.Academic;

namespace SchoolManager.Application.Academic;

public interface ICourseApplication
{
    int CreateCourse(Course course);
    IList<Course> GetAllCourses();
    Course GetById(int id);
} 