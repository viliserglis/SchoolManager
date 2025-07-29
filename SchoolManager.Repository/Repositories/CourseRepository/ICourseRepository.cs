using SchoolManager.Models.Academic;

namespace SchoolManager.Repository.Repositories.CourseRepository;

public interface ICourseRepository
{
    int CreateCourse(Course course);
    IList<Course> GetAllCourses();
} 