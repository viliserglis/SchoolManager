using SchoolManager.Models.Demographics;

namespace SchoolManager.Application.Demographics;

public class StudentApplication : IStudentApplication
{
    public string GetFullName(Student student)
    {
        return student.FirstName + " " + student.LastName;
    }
}
