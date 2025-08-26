using SchoolManager.Repository.Repositories.StudentCourseMapRepository;

namespace SchoolManager.Application.Academic;

public class StudentCourseMapApplication(IStudentCourseMap studentCourseMapRepository) : IStudentCourseMapApplication
{
    public void EnrollStudentInCourse(int studentId, int courseId)
    {
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