using SchoolManager.Repository.Repositories.StudentCourseMapRepository;

namespace SchoolManager.Application.Academic;

public class StudentCourseMapApplication(IStudentCourseMap studentCourseMapRepository) : IStudentCourseMapApplication
{
    public void EnrollStudentInCourse(int studentId, int courseId)
    {
        var students = studentCourseMapRepository.GetStudentIds(courseId);
        if (students.Any(student => student == studentId))
        {
            throw new InvalidOperationException("Student already enrolled in this course");
        }
        
        studentCourseMapRepository.Create(studentId, courseId);
    }

    public int[] GetStudentsByCourse(int courseId)
    {
        return studentCourseMapRepository.GetStudentIds(courseId);
    }

    public int[] GetCoursesByStudent(int studentId)
    {
        return studentCourseMapRepository.GetCourseIds(studentId);
    }
}