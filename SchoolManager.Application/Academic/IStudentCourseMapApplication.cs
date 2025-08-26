namespace SchoolManager.Application.Academic;

public interface IStudentCourseMapApplication
{
    void EnrollStudentInCourse(int studentId, int courseId);
    int[] GetStudentsByCourse(int courseId);
    int[] GetCoursesByStudent(int studentId);
}