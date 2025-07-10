using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SchoolManager.Repository.Infrastructure;
using SchoolManager.Repository.Repositories.StudentRepository;
using SchoolManager.Repository.Repositories.TeacherRepository;

namespace SchoolManager.Repository.IoC;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepository(this IServiceCollection services,  IConfiguration configuration)
    {
        services.Configure<RepositoryConfiguration>(configuration.GetSection("RepositoryConfiguration"));
        services.AddTransient<IConnectionFactory, ConnectionFactory>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<ITeacherRepository, TeacherRepository>();
        using var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<RepositoryConfiguration>>() ??
                      throw new ArgumentNullException(nameof(RepositoryConfiguration));

        if (!options.Value.IsAutoMigrate) return services;

        services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddPostgres()
                .WithGlobalConnectionString(options.Value.ConnectionString)
                .ScanIn(typeof(RepositoryConfiguration).Assembly)
                .For.All())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        var serviceProvider = services.BuildServiceProvider();
        var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>() ??
                              throw new ArgumentNullException(nameof(IMigrationRunner));

        migrationRunner.MigrateUp();
        return services;
    }
}