using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Academic;
using SchoolManager.Models.Academic;
using SchoolManager.Models.DTO.Academic;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseApplication courseApplication, ILogger<CourseController> logger)
{
    [HttpPost]
    [Route("create")]
    public int CreateCourse([FromBody] Course course)
    {
        logger.LogInformation("Creating new course with name: {CourseName}", course.Name);
        try
        {
            var courseId = courseApplication.CreateCourse(course);
            logger.LogInformation("Successfully created course with ID: {CourseId}", courseId);
            return courseId;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating course with name: {CourseName}", course.Name);
            throw;
        }
    }
    
    [HttpGet]
    [Route("all")]
    public IList<Course> GetAllCourses()
    {
        logger.LogInformation("Fetching all courses");
        try
        {
            var courses = courseApplication.GetAllCourses();
            logger.LogInformation("Successfully fetched {CourseCount} courses", courses.Count);
            return courses;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching all courses");
            throw;
        }
    }

    [HttpGet]
    [Route("{id}")]
    public CourseDTO GetById(int id)
    {
        logger.LogInformation("Fetching course with ID: {CourseId}", id);
        try
        {
            var course = courseApplication.GetById(id);
            if (course != null)
            {
                logger.LogInformation("Successfully fetched course with ID: {CourseId}", id);
            }
            else
            {
                logger.LogWarning("Course not found with ID: {CourseId}", id);
            }
            return course;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching course with ID: {CourseId}", id);
            throw;
        }
    }
} 