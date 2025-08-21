using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Demographics;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Repositories.TeacherRepository;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(ITeacherApplication TeacherApplication, ILogger<TeacherController> logger)
{
    [HttpPost]
    [Route("Teacher")]
    public int CreateTeacher([FromBody] Teacher teacher)
    {
        logger.LogInformation("Creating new teacher: {FirstName} {LastName}", teacher.FirstName, teacher.LastName);
        try
        {
            var teacherId = TeacherApplication.CreateTeacher(teacher);
            logger.LogInformation("Successfully created teacher with ID: {TeacherId}", teacherId);
            return teacherId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating teacher: {FirstName} {LastName}", teacher.FirstName, teacher.LastName);
            throw;
        }
    }
    
    [HttpGet]
    [Route("all")]
    public IList<Teacher> GetAllTeachers()
    {
        logger.LogInformation("Fetching all teachers");
        try
        {
            var teachers = TeacherApplication.GetAllTeachers();
            logger.LogInformation("Successfully fetched {TeacherCount} teachers", teachers.Count);
            return teachers;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching all teachers");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public Teacher GetById(int id)
    {
        logger.LogInformation("Fetching teacher with ID: {TeacherId}", id);
        try
        {
            var teacher = TeacherApplication.GetById(id);
            if (teacher != null)
            {
                logger.LogInformation("Successfully fetched teacher with ID: {TeacherId}", id);
            }
            else
            {
                logger.LogWarning("Teacher not found with ID: {TeacherId}", id);
            }
            return teacher;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching teacher with ID: {TeacherId}", id);
            throw;
        }
    }
}