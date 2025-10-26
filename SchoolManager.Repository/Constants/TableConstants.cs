namespace SchoolManager.Repository.Constants;

public static class TableConstants
{
    public static readonly  string Student = "students";
    public static readonly string Teacher = "Teacher";
    public static readonly string Course = "courses";
    public static readonly string StudentCourse = "student_course";
    public static readonly string User = "users";
    public static readonly string Permissions = "permissions";
    public static readonly string RolePermissions = "role_permissions";
    public static readonly string Roles = "roles";
    public static string WithSchema(this string table, string schema) => $"{schema}.{table}";
}