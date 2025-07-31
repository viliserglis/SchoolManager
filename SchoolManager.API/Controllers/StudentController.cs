using Microsoft.AspNetCore.Mvc;
using SchoolManager.Application.Demographics;
using SchoolManager.Models.Demographics;
using SchoolManager.Repository.Repositories.StudentRepository;

namespace SchoolManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentApplication studentApplication)
{
    [HttpPost]
    [Route("create")]
    public int CreateStudent([FromBody] Student student)
    {
        return studentApplication.CreateStudent(student);
    }
    
    [HttpGet]
    [Route("all")]
    public IList<Student> GetAllStudents()
    {
        return studentApplication.GetAllStudents();
    }
    
    [HttpGet]
    [Route("{id}")]
    public Student GetById(int id)
    {
        return studentApplication.GetById(id);
    }
}

