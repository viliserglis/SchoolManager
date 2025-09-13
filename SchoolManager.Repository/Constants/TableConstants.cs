namespace SchoolManager.Repository.Constants;

public static class TableConstants
{
    public static readonly  string Student = "students";
    public static readonly string Teacher = "Teacher";
    public static readonly string Course = "courses";
    public static readonly string StudentCourse = "student_course";
    public static string WithSchema(this string table, string schema) => $"{schema}.{table}";
}