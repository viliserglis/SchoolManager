using SchoolManager.Models.Academic;
using SchoolManager.Repository.Repositories.CourseRepository;

namespace SchoolManager.Application.Academic;

public class CourseApplication(ICourseRepository courseRepository) : ICourseApplication
{
    public int CreateCourse(Course course)
    {
        return courseRepository.CreateCourse(course);
    }

    public IList<Course> GetAllCourses()
    {
        return courseRepository.GetAllCourses();
    }
} 