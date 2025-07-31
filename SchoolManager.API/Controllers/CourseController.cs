using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Academic;
using SchoolManager.Models.Academic;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(ICourseApplication courseApplication)
{
    [HttpPost]
    [Route("create")]
    public int CreateCourse([FromBody] Course course)
    {
        return courseApplication.CreateCourse(course);
    }
    
    [HttpGet]
    [Route("all")]
    public IList<Course> GetAllCourses()
    {
        return courseApplication.GetAllCourses();
    }

    [HttpGet]
    [Route("{id}")]
    public Course GetById(int id)
    {
        return courseApplication.GetById(id);
    }
} 