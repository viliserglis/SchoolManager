using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Academic;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentCourseMapController(IStudentCourseMapApplication studentCourseMapApplication, ILogger<StudentCourseMapController> logger)
{
    [HttpPost]
    [Route("enroll")]
    public IActionResult EnrollStudentInCourse([FromBody] EnrollmentRequest request)
    {
        logger.LogInformation("Enrolling student {StudentId} in course {CourseId}", request.StudentId, request.CourseId);
        try
        {
            studentCourseMapApplication.EnrollStudentInCourse(request.StudentId, request.CourseId);
            logger.LogInformation("Successfully enrolled student {StudentId} in course {CourseId}", request.StudentId, request.CourseId);
            return new OkResult();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error enrolling student {StudentId} in course {CourseId}", request.StudentId, request.CourseId);
            throw;
        }
    }
    
    [HttpGet]
    [Route("course/{courseId}/students")]
    public int[] GetStudentsByCourse(int courseId)
    {
        logger.LogInformation("Fetching students for course {CourseId}", courseId);
        try
        {
            var students = studentCourseMapApplication.GetStudentsByCourse(courseId);
            logger.LogInformation("Successfully fetched {StudentCount} students for course {CourseId}", students.Length, courseId);
            return students;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching students for course {CourseId}", courseId);
            throw;
        }
    }
    
    [HttpGet]
    [Route("student/{studentId}/courses")]
    public int[] GetCoursesByStudent(int studentId)
    {
        logger.LogInformation("Fetching courses for student {StudentId}", studentId);
        try
        {
            var courses = studentCourseMapApplication.GetCoursesByStudent(studentId);
            logger.LogInformation("Successfully fetched {CourseCount} courses for student {StudentId}", courses.Length, studentId);
            return courses;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching courses for student {StudentId}", studentId);
            throw;
        }
    }
}

public class EnrollmentRequest
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}