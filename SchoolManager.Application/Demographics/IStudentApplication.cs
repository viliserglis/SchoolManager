using SchoolManager.Models.Demographics;

namespace SchoolManager.Application.Demographics;

public interface IStudentApplication
{
    public string GetFullName(Student student);
}