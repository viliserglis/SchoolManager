using SchoolManager.Models.Academic;
using SchoolManager.Models.DTO.Academic;
using SchoolManager.Repository.Repositories.CourseRepository;
using SchoolManager.Repository.Repositories.TeacherRepository;

namespace SchoolManager.Application.Academic;

public class CourseApplication(ICourseRepository courseRepository, ITeacherRepository teacherRepository) : ICourseApplication
{
    public int CreateCourse(Course course)
    {
        var teacher = teacherRepository.GetById(course.TeacherId);
        if (teacher == null)
        {
            throw new Exception("Teacher not found");
        }
        return courseRepository.CreateCourse(course);
    }

    public IList<Course> GetAllCourses()
    {
        return courseRepository.GetAllCourses();
    }

    public CourseDTO GetById(int id)
    {
        var course = courseRepository.GetById(id);
        var courseDTO = new CourseDTO(course);
        courseDTO.Teacher = teacherRepository.GetById(course.TeacherId);
        return courseDTO;
    }
} 