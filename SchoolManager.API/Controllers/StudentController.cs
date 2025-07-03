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
    [Route("student")]
    public int CreateStudent([FromBody] Student student)
    {
        return studentApplication.CreateStudent(student);
    }
}

