namespace SchoolManager.Models.Demographics;

public abstract class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; }  = String.Empty;
}