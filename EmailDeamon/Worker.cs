using EmailDeamon.Services;
using Microsoft.Extensions.Options;

namespace EmailDeamon;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly StudentService _studentService;
    private readonly WorkerSettings _settings;

    public Worker(ILogger<Worker> logger, StudentService studentService, IOptions<WorkerSettings> settings)
    {
        _logger = logger;
        _studentService = studentService;
        _settings = settings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Email Daemon startēts. Pārbauda jaunus skolēnus ik pēc {Minutes} minūtēm.", _settings.CheckIntervalMinutes);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Pārbauda jaunus skolēnus... {Time}", DateTimeOffset.Now);

                var newStudents = await _studentService.GetNewStudentsAsync();

                if (newStudents.Any())
                {
                    foreach (var student in newStudents)
                    {
                        _logger.LogInformation(
                            "ATRASTS JAUNS SKOLĒNS! ID: {Id}, Vārds: {FirstName} {LastName} - E-pasts nosūtīts (simulēts)",
                            student.Id,
                            student.FirstName,
                            student.LastName);
                    }
                }
                else
                {
                    _logger.LogInformation("Nav atrasti jauni skolēni.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kļūda, pārbaudot jaunus skolēnus");
            }

            // Wait for the configured interval (5 minutes)
            await Task.Delay(TimeSpan.FromMinutes(_settings.CheckIntervalMinutes), stoppingToken);
        }
    }
}

public class WorkerSettings
{
    public int CheckIntervalMinutes { get; set; } = 5;
}