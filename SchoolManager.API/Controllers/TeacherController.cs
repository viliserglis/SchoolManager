using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Demographics;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Repositories.TeacherRepository;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(ITeacherApplication TeacherApplication)
{
    [HttpPost]
    [Route("Teacher")]
    public int CreateTeacher([FromBody] Teacher teacher)
    {
        return TeacherApplication.CreateTeacher(teacher);
    }
    
    [HttpGet]
    [Route("all")]
    public IList<Teacher> GetAllTeachers()
    {
        return TeacherApplication.GetAllTeachers();
    }
    
    [HttpGet]
    [Route("{id}")]
    public Teacher GetById(int id)
    {
        return TeacherApplication.GetById(id);
    }
}