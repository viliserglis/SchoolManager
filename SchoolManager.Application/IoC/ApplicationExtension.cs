using Microsoft.Extensions.DependencyInjection;
using SchoolManager.Application.Academic;
using SchoolManager.Application.Demographics;

namespace SchoolManager.Application.IoC;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IStudentApplication, StudentApplication>();
        services.AddTransient<ITeacherApplication, TeacherApplication>();
        services.AddTransient<ICourseApplication, CourseApplication>();
        services.AddTransient<IStudentCourseMapApplication, StudentCourseMapApplication>();
        
        return services;
    }
}