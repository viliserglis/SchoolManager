using SchoolManager.Models.Academic;
using SchoolManager.Models.Demographics;

namespace SchoolManager.Models.DTO.Academic;

public class CourseDTO 
{
    public CourseDTO(Course course)
    {
        ID = course.ID;
        Name = course.Name;
        Room = course.Room;
        Capacity =  course.Capacity;
    }
    public int ID { get; set; }
    public string Name { get; set; }
    public string Room { get; set; }
    public int Capacity { get; set; }
    public Teacher Teacher { get; set; }
    
}