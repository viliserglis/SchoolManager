using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Demographics;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Repositories.StudentRepository;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentApplication studentApplication, ILogger<StudentController> logger)
{
    [HttpPost]
    [Route("create")]
    public int CreateStudent([FromBody] Student student)
    {
        logger.LogInformation("Creating new student: {FirstName} {LastName}", student.FirstName, student.LastName);
        try
        {
            var studentId = studentApplication.CreateStudent(student);
            logger.LogInformation("Successfully created student with ID: {StudentId}", studentId);
            return studentId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating student: {FirstName} {LastName}", student.FirstName, student.LastName);
            throw;
        }
    }
    
    [HttpGet]
    [Route("all")]
    public IList<Student> GetAllStudents()
    {
        logger.LogInformation("Fetching all students");
        try
        {
            var students = studentApplication.GetAllStudents();
            logger.LogInformation("Successfully fetched {StudentCount} students", students.Count);
            return students;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching all students");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public Student GetById(int id)
    {
        logger.LogInformation("Fetching student with ID: {StudentId}", id);
        try
        {
            var student = studentApplication.GetById(id);
            if (student != null)
            {
                logger.LogInformation("Successfully fetched student with ID: {StudentId}", id);
            }
            else
            {
                logger.LogWarning("Student not found with ID: {StudentId}", id);
            }
            return student;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching student with ID: {StudentId}", id);
            throw;
        }
    }
}

