namespace SchoolManager.Models.Academic;

public class Course
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Room { get; set; }
    public int Capacity { get; set; }

    public int TeacherId { get; set; }
} 